The terrain needs to be able to have any size, have a lot of irregular curves so it looks natural and that we could add chunks to it, with emphasis on animating the terrain when it grows, and low poly.

> We need to research some alternatives before coding.

**RESEARCH**
I finally found something I like how it looks and that lets me add irregular spots to the mesh.

[Procedurally Generated Low-Poly Terrains in Unity](https://www.youtube.com/watch?v=sRn8TL3EKDU) by Kristin Lague

 We could create a secondary mesh every time the user wants to increase the forest size, animate it and then when the game restarts it can be added to the main mesh, that way we can load the entirety of the mesh when it opens and have flexibility when adding the new discovered regions.

The bad thing is that we must store the points generated since they are randomly generated and could happen at anytime.

Luckily that is not a hard thing to do.

> Now that we have the terrain, let's clean the repo of old code and start coding the real deal.

I have played with the algorithm and I understand how it is working, the next thing I would like to do is calculate all the things needed for the terrain in files and then create chunks of triangles so we can have better performance on big forests.

- Create Poisson Disc Sample points.
- Triangulate using Delaunay.
- Save points and triangles in files.
- Load the save data separating the triangles in chunks.

After we get this working we can start moving the configurations to SO and start using DI to separate in Managers and Controllers.

***30/11/2024***
After some experimentation I finally came with a terrain I like, we are creating a circle terrain using Perlin noise, then we are making another one but inverted in Y so it works as the bottom of the island.

I still need to solve the issue when the Perlin noise creates holes in the middle, making it having negative numbers, those don't let the top and bottom sides snap.