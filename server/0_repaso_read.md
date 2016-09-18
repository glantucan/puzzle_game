


####  Tema 0
#  Repaso y refuerzo de programación en C# con Unity



----


### Tema 0: Repaso

### 0.0.- Mecánicas de juego. 


## Un paseo por las mecánicas del juego

  *  Movimiento del player.
  *  Caida (no hay salto).
  *  Switches (interruptores) y sus acciones.
  *  Inventario (De momento sólo coge piedras).
  *  Cámara (seguimiento y rotación).
  *  Fin de nivel.
  

----


### Tema 0: Repaso

### 0.1.-Preparar el proyecto


## Organización del proyecto
 
Podéis descargar el proyecto inicial [aquí](https://github.com/glantucan/puzzle_game/archive/00_BareProject.zip).

*  Colocación ventanas de Unity (y guardado). 
*  Nombres de carpetas y archivos en `Assets`.  
*  GameObjects anidados para organizar la jerarquía de escena
*  Configuración para trabajar con MonoDevelop.
  * Descarga [este archivo](https://drive.google.com/open?id=0B97mmF9E10p3SUVCQm9yXy03YXM).
  * Descomprímelo en una carpeta temporal.
  * Copia las carpetas que hay dentro a  
  `c:\\Users\your_user_name\AppData\Roaming\MonoDevelop-Unity-5.0`.  
Borra lo que haya dentro de esa carpeta previamente.


----


### Tema 0: Repaso

### 0.2.- GameObjects y Componentes


## Recordatorio básico
*  **GameObject**: Pieza básica para construir elementos de juego
*  **Componente**: Configura el comportamiento y aspecto del GameObject.
	*  Componente `Transfrom` (posición, rotación y escala)
  	*  Otros componentes típicos  
		* `MeshFilter` -> Definición de la malla (Vértices y aristas)
		* `MeshRenderer` -> material: texturas, color, shaders...
		* `Collider` -> *Solidez* del game object.
		* `Rigidbody` -> Propiedades *físicas* del gameobject
		* ...
		*  Scripts (que heredan las propiedades de `MonoBehaviour`)  
		Cuando no haya un componente prefabricado que haga lo que queremos, lo creamos nosotros en C#.  
<br>  

###  Nuestro juego tendrá inicialmente:
*  Muchos gameobjects
*  Unos cuantos componentes en cada gameobject &rArr; Muchísimos componentes
*  Un sólo compnente de tipo Script (`Player.cs`)


----


### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour


### Primero vamos a hacer las cosas mal. 
#### &rArr; Para que entendáis por qué hay que hacerlas bien.  
*  Vamos a meter todas las mecánicas del juego en un sólo componente.  
*  Acabaremos con un script de más de 300 líneas
  * Veremos que nos falta mucho por hacer para acabar el juego
  * Nos daremos cuenta de que es prácticamente imposible orientarse entre tanto código.
  * Hacer cambios en una mecánica afecta al código de las otras. Modificar un pequeño detalle desencadena cascadas de cambios que no acaban nunca.

<p></p>



---




### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour


### Después haremos las cosas regular
*  
Para organizar nuestro código y nuestra cabeza 
	* Separaremos cada pieza de las mecánicas en funciones diferentes.
	* Cada función debe funcionar como una caja negra.
		* La ejecuto, le doy la info que necesita.
		* Hace **sólo** su tarea.
		* Me devuelve el control.
			* Puedo hacer que me devuelva información útil si lo necesito.



### Y después haremos las cosas bien 
*   Divide y vencerás: 
	* Cada pieza del juego va en un componente diferente.
	* Cada pieza es lo más independiente de las otras que se pueda.
		* Veremos que todo es mucho más claro y manejable
		* Podremos cambiar y añadir cosas sin cambiar código de otras piezas.
	  
  


---


### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour


### Creando el componente `Player`
1. Crear un nuevo Script
	*  En la carpeta *Scripts* de nuestros *Assets* 
		*  Click con el botón derecho 
			*  *Create* 
				*  *C# Script*  
					*  Ponemos `Player` como nombre  
					
	El nombre de la clase coincide con el nombre del archivo.   
	**Esto debe cumplirse siempre**.  
	Si no el script no funcionará.  
4. Arrastrad el script desde *Assets/Scripts* al gameobject *Player* 


---


### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour



Para editar el Script tenemos varias opciones:
* Doble click sobre él en Assets
* Doble click sobre su nombre en el componente del gameobject que lo contiene.

```cs
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
```

Los scripts que creamos derivan por defecto de [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html).  
=> Todas las funciones y propiedades de [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) están incluidas en nuestro script aunque no se vean.


---


### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour


## Funciones evento básicas

Las funciones evento son ejecutadas automáticamente por Unity cuando el evento del mismo nombre ocurre.  
Cuidado al escribir su nombre. Si lo hacemos mal no funcionan.

**`Start()`**  

```cs
// Use this for initialization
void Start () {

}
```

* Se ejecuta cuando se activa el GameObject que contiene el componente. 
* Sólo la primera vez que se activa
* Aquí se le dan los valores iniciales a todas las propiedades del componente.

**`Update()`**  

```cs
// Update is called once per frame
void Update () {

}
```
* Se ejecuta automáticamente en cada fotograma (cada 1/60 de segundo aprox.)
* Aquí actualizamos posiciones, comprobamos condiciones de las mecánicas de juego, actualizamos variables, temporizadores, etc.



---



### Tema 0: Repaso

### 0.3.- Scripts y MonoBehaviour


## Otros elementos de nuestros scripts

```cs
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// PROPERTIES DEFINITION:
	// ----------------------
	// Variables that can be used in all methods of the script
	// We can make them public
	public float speed; // this property will be shown in the inspector
	// Or we can make them private
	private Rigidbody rb; // this property will be hidden in the inspector. More details on public and private later.
	
	// METHODS DEFINITION:
	// --------------------
	// This is a method (also called function). In this case it's an event function or method. Use this one for initialization
	void Start () {
		// We can give values to defined properties inside any method.
		// Here we are using the inherited method GetComponent to get the Rigidbody component of the gameobject containig this script
		rb = this.GetComponent<Rigidbody>();
		// We can call another methods defined in this class so they are executed when this method (Start) is executed
		this.printValue("Initial speed: ", speed);
		// printValue is a method defined below.
	}

	// Update is also an event function. It is called once per frame
	void Update () {
		// We can also define local variables that only exist iside this method
		Vector3 velocity = Vector3.forward * this.speed;
		// and give them a value. We can use pre-defined variables or properties for that. Above we are using the speed property.
		// As it is public, its value was initialized in the inspector. 
		// We can also set properties defined in other components as below:
		rb.velocity = velocity;
	}
	
	// We can also define our own methods with the name and parameters we consider appropiate
	// so that we can execute them whenever we need from other methods of the class.
	// This is not an event function. It is not called authomatically by Unity. It is executed because 
	// we wrote a call sentence for it on the Start method.
	void printValue(string label, System.Object value) {
		Debug.Log(label + ": " + value);
	}
}
```



----





### Tema 0: Repaso

### 0.4.- Movimiento básico



##  Dos formas de mover un GameObject (Con velocidad constante) <br><br>

#### A través del componente **Transform**
```cs
public float speed = 10F;

void Update() {
	Vector3 direction = Vector3.forward; // Por ej. 
	Vector3 velocity = direction * speed;
	this.transform.position += velocity * Time.deltaTime;
}
```
<br><br><br><br>

#### A través del componente **Rigidbody**
```cs
public float moveVel = 10F;
private Rigidbody rb;

void Start () {
	this.rb = this.GetComponent<Rigidbody>();
}

void Update () {
	Vector3 direction = Vector3.forward;
	this.rb.velocity = direction * moveVel;
}
```
 El vector `direction` debe tener longitud 1 para que `moveVel` actúe como esperamos.

 Podemos utilizar `direction.normalized` en lugar de `direction` si no lo es.