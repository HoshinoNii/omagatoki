# Omatoki
ŌmaToki is a tower defense game set in a fantasy japanese world upon floating islands where you fend off demonic spirits attacking your castle and you have to build defenses and execute spells to stop their approach.

## Version 0.2 Object Pooling Guide

### Creating a new Pool
- In order to impement a new Projectile, create a new Pool Data Via: Create>Tower Defense>PoolData and assign a tag, GameObject and Size into it
   - tag, identifier for PoolManager.cs to obtain
   - GameObject for PoolManager.cs to spawn
   - How many of such GameObject to add into Pool
   
- Assign the tag into PoolIndex.cs

### Integrating the Pool into the system
   - **For Projectiles**, change the Projectile Type inside Tower.cs
   - **For Blueprints** Change What To Build in Build_Blueprint.cs
