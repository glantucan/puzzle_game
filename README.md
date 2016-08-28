# puzzle_game
A small Unity basics review example and start point to go to a modular design 


## Detección de colisiones y triggers. 
### Cuando usar un *collider*
Cuando el gameObject que lo contiene ha de chocar con otros objetos y no atravesarlos o ser atravesado por ellos.	
 
En este caso, si queremos detectar y reaccionar a los choques, podemos detectar los eventos `OnCollisionEnter`, `OnCollisionStay` y `OnCollisionExit` utilizando las *funciones evento* correspondientes en un script de uno de los gameobjects implicados (en cual de ellos puede depender de si tienen un componente Rigidbody y de como esté configurado). 
Por ejemplo:
```cs
void OnCollisionEnter(Collision col) {
    Debug.Log(col.gameObject.name + " has collided with " + this.name);	
}
```
El objecto recibido como parámetro en las funciones evento es siempre un `Collision`. Es importante que conozcas bien la información que llega en ese objeto: puedes ver que propiedades tiene [aquí]( https://docs.unity3d.com/ScriptReference/Collision.html)

### Cuando usar un *trigger*
Cuando queremos detectar si un gameObject entra en una determinada zona, y que nuestro programa actue en consecuencia, pero no queremos que choque con nada. 
 
Para detectar cuando esto ocurre deberemos utilizar en este caso los eventos `OnTriggerEnter`, `OnTriggerStay` u `OnTriggerExit`. Por ejemplo:
```cs
void OnTriggerEnter(Collider otherCol) {
  Debug.Log(otherCol.name + " has entered the trigger zone of " + this.name);	
}
```
Fíjate que en el caso de los triggers el parámetro recibido por la función evento es simplemente el otro collider.

### ¿En que gameobject funcionan las funciones evento?

Depende de si tienen ambos un componente Rigidbody y de como estén estos configurados (Propiedad `isKinematic` a `true` o `false`).

En base a lo anterior se definen tres tipos de **Collider** o **Trigger** :

* **Estático** -> No tiene **Rigidbody**
* **Dinámico** -> Tiene **Collider** y **Rigidbody**. `isKinematic = false`
* **Cinemático** -> Tiene **Collider** y **Rigidbody**. `isKinematic = true`

Teniendo presentes estas definiciones las siguientes tablas resumen cuando se ejecutan las funciones evento en ambos gameObject implicados.

<br>
#### Para funciones evento de colisión:
**`OnCollisionEnter`, `OnCollisionStay` y `OnCollisionExit`**

![](https://www.filepicker.io/api/file/5qBv1Lc3QfqVlcnM7VlE)

#### Para funciones evento de trigger: 
**`OnTriggerEnter`, `OnTriggerStay` u `OnTriggerExit`**

![](https://www.filepicker.io/api/file/ouZlYdVjQvGkJR5E1f4Q)
