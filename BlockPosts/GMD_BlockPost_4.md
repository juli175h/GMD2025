# Dev update 2: Abilities, Weapons & UI
In this post, I’ll walk through the work completed for Milestone 2 of my GMD1 game project. The goal was to add an area-effect weapon, an active speed boost ability, and a short invincibility ability and make UI for game overlay.

## Splatter Weapon

The second weapon is an area-of-effect splatter attack that functions similarly to the bullet weapon in terms of attack timing but behaves very differently in action. Instead of firing in a straight line, it shoots a projectile in an arc in a random direction near the player. When it lands, it creates a damaging puddle that hurts enemies over time while they stand in it.  
The projectile’s arc is calculated by interpolating between the start and target positions using `Mathf.Sin()` to simulate height, giving it a natural arc-like trajectory.
```csharp
float height = Mathf.Sin(progress * Mathf.PI) * arcHeight;
``` 
To make the projectile face the direction it's moving, I calculate a movement vector and apply rotation using `Mathf.Atan2()` and `Quaternion.Euler()`:
```csharp
Vector3 direction = (newPos - transform.position).normalized;
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
transform.rotation = Quaternion.Euler(0, 0, angle);
``` 
The actual damage-over-time effect is handled by the `SplatterPool` script attached to the splatter prefab. It uses `OnTriggerStay2D()` to track enemies inside the zone, storing individual timers for each one in a `Dictionary<EnemyController, float>` to apply damage at regular intervals. Once an enemy’s timer exceeds the tick rate, it takes damage and the timer resets.
```csharp
private void OnTriggerStay2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            if (!damageTimers.ContainsKey(enemy))
                damageTimers[enemy] = 0f;

            damageTimers[enemy] += Time.deltaTime;

            if (damageTimers[enemy] >= tickRate)
            {
                enemy.TakeDamage(damage);
                damageTimers[enemy] = 0f;
            }
        }
    }
}
``` 
`OnTriggerExit2D()` is used to remove enemies from the dictionary once they leave the zone.  
```csharp
private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null && damageTimers.ContainsKey(enemy))
        {
            damageTimers.Remove(enemy);
        }
    }
}
``` 
## Shield and Speed Boost

The other two weapons I want to implement are a shield that switches on and off and makes the player invincible, and a speed boost that can be activated with a user input. The code for these isn’t that exciting so I won’t go too into depth about it. I will though, mention a little about the speed boost, as it is a bit different from the other weapons.  
The input is handled using Unity's Input System, similar to movement. Once the ability is unlocked, the `InputAction` is enabled and listens for a button press.  
```csharp
if(level > 0)
{
    SpeedBoostAction.Enable();
    SpeedBoostAction.performed += OnSpeedBoostPressed;
    SpeedBoostUI.gameObject.SetActive(true);
}
``` 
When triggered, it starts a coroutine called `SpeedBoost()`. This temporarily multiplies the player's movement speed, waits for a set duration, and then restores the original speed.
```csharp
private void OnSpeedBoostPressed(InputAction.CallbackContext context)
{
    if (isUp)
    {
        StartCoroutine(SpeedBoost());
        isUp = false;
    }
}
IEnumerator SpeedBoost()
{
    SpeedBoostUI.gameObject.GetComponent<Image>().color = selectedColor;
    float oldSpeed = player.movementSpeed;
    player.movementSpeed = oldSpeed * speedAmp;
    yield return new WaitForSeconds(duration);
    player.movementSpeed = oldSpeed;
    EventSystem.current.SetSelectedGameObject(null);
    SpeedBoostUI.gameObject.GetComponent<Image>().color = disabledColor;
}
``` 

## Slider UI

A core part of the game’s UI is built using Unity’s `Slider` component, which is reused across the XP bar, the player’s health bar, and each enemy’s health bar. As a child component they have a colored image dragged into the fill property of the slider component in the inspector.

The bar script attached have two public methods, `SetMaxValue(int value)` and `public void SetValue(float value)`. These methods are called from other scripts to update the bar’s values during gameplay, for example, when the player gains XP.
```csharp
public void SetMaxValue(int value)
{
    slider.maxValue = value;
    if(fill != null)
    fill.color = gradient.Evaluate(1f);
}
public void SetValue(float value)
{
    slider.value = value;
    if (fill != null)
        fill.color = gradient.Evaluate(slider.normalizedValue);
}
``` 
It’s also possible to apply a gradient with the script by adding a reference to the fill in the inspector and making a gradient. This is done for the enemies’ health bar, making it change from green to red according to the value.

## Conclusion

Now with all the weapons/abilities done, the game has all the functionality needed to start developing the actual gameplay.