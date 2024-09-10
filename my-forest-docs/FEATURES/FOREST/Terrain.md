The terrain needs to be able to have any size, have a lot of irregular curves so it looks natural and that we could add chunks to it, with emphasis on animating the terrain when it grows, and low poly.

> We need to research some alternatives before coding.

**RESEARCH**
I finally found something I like how it looks and that lets me add irregular spots to the mesh.

[Procedurally Generated Low-Poly Terrains in Unity](https://www.youtube.com/watch?v=sRn8TL3EKDU) by   
Kristin Lague

 We could create a secondary mesh every time the user wants to increase the forest size, animate it and then when the game restarts it can be added to the main mesh, that way we can load the entirety of the mesh when it opens and have flexibility when adding the new discovered regions.

The bad thing is that we must store the points generated since they are randomly generated and could happen at anytime.

Luckily that is not a hard thing to do.

> Now that we have the terrain, let's clean the repo of old code and start coding the real deal.