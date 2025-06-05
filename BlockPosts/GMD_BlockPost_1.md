# Rollaball Assignment 

The Rollaball assignment for the Game Develoupment course is my second time ever develouping games in Unity. My only other time was about 5 years ago where I had a 5 day masterclass at HTX. At that time I was not nearly as good at programming as I am now. and I've found the learning curve much easier this time around.

I've followed the Rollaball tutorial on the Unity website and I feel that this has been a great introduction on how to use Unity, and I found the tutorial very easy to follow. I did however have my first challenge very early on in the project, when getting my ball to roll. The tutorial made it look very easy with just adding a player input component, and then it auto generated a Input Action Asset, which they didn't explain much about in the tutorial.

![Input Action Asset Icon](image.png)

I could for the longest time not figure out why my ball didn't roll, when I followed the tutorial exactly as shown. It turns out that my program did not auto generate this file. Luckily I could make one myself, and with that solved, my ball rolled. afterwards I didn't have any other problems with the tutorial.

## Level Expansion

When it came to expanding the game, I first decided to make another level to the game. This new level is a bit harder, it has more points to pick up, there is a second enemy. I also made a lot spaces in the level where only the enemy could walk through to make the level even more difficult.

I did this by making another scene, as well as a scene manager script on an empty game object called Game Manager. In the first scene i added a butten, that takes the player to the next level when clicked.

## Adding a Timer

Lastly I also added a timer, as I thougt it could make the game more competetive and more fun, since getting all the points otherwise would be a little one and done. I made a singleton timer class. I needed the value of the timer to transfer between scenes, and it took some time to make it work right, as it at first wouldn't set the text properly when changing scenes.

Eventually i got it to work by changing from getting the timer instance by searching the scene for the component to just making the instance public and getting it from the timer singleton class.

## Conclusion

Overall, I have learned a lot during this small asignment and I would recommend this turorial to anyone just starting out game develoupment in Unity.
