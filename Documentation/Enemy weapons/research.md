#Enemy weapon research
We have different kind of weapons for enemies. Saving them all as different gameobjects is possible, but we can do it easier. All weapons have a type (semi-automatic, automatic, charge). The only things that they differ in are their texture, weapon stats (damage, fire rate), and bullets. Because of this we can use **Scriptable objects**.