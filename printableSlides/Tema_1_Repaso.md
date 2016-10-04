
####  Tema 1
#  Repaso y refuerzo de programación en C# con Unity



----


### Tema 1: Repaso

### 1.0.- Mecánicas de juego. 


## Un paseo por las mecánicas del juego

  *  Movimiento del player.
  *  Caida (no hay salto).
  *  Switches (interruptores) y sus acciones.
  *  Inventario (De momento sólo coge piedras).
  *  Cámara (seguimiento y rotación).
  *  Fin de nivel.
  

----


### Tema 1: Repaso

### 1.1.-Preparar el proyecto


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


### Tema 1: Repaso

### 1.2.- GameObjects y Componentes


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


### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour


### Primero vamos a hacer las cosas mal. 
#### &rArr; Para que entendáis por qué hay que hacerlas bien.  
*  Vamos a meter todas las mecánicas del juego en un sólo componente.  
*  Acabaremos con un script de más de 300 líneas
  * Veremos que nos falta mucho por hacer para acabar el juego
  * Nos daremos cuenta de que es prácticamente imposible orientarse entre tanto código.
  * Hacer cambios en una mecánica afecta al código de las otras. Modificar un pequeño detalle desencadena cascadas de cambios que no acaban nunca.

<p></p>



---




### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour


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


### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour


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


### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour



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


### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour


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



### Tema 1: Repaso

### 1.3.- Scripts y MonoBehaviour


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





### Tema 1: Repaso

### 1.4.- Movimiento básico



##  Dos formas de mover un GameObject (Con velocidad constante) 
<br>
 <button class="warning">WARNING!</button> A partir de ahora necesitas entender como operar con vectores. <br><br>
#### A través del componente **Transform**
```cs
public float speed;

void Update() {
	Vector3 direction = Vector3.forward; // Por ej. 
	Vector3 velocity = direction * speed;
	this.transform.position += velocity * Time.deltaTime;
}
```
<br><br><br><br>

#### A través del componente **Rigidbody**
```cs
public float speed;
private Rigidbody rb;

void Start () {
	this.rb = this.GetComponent<Rigidbody>();
}

void Update () {
	Vector3 direction = Vector3.forward;
	this.rb.velocity = direction * speed;
}
```
 El vector `direction` debe tener longitud 1 para que `moveVel` actúe como esperamos.

 Podemos utilizar `direction.normalized` en lugar de `direction` si no lo es.


Note: 
* Stop here and show some examples. Make them do some exercises before going further.





---




### Tema 1: Repaso

### 1.4.- Movimiento básico


## Uso del Rigidbody 

Para usar las físicas de Unity con un gameobject tenemos que añadirle un componente *Rigidbody*.
* Con el gameobject seleccionado
*  *Inspector* 
	*  *Add Component*
	 	*  *Physics*
		 	*   *Rigidbody*

En este caso (no es por norma general):
* Desactivaremos *Use Gravity* 
* Seleccionaremos *Freeze rotation* para los ejes X y Z  
  (Para que no se nos "caiga" el personaje al caminar)  
* Dejamos las demás propiedades del componente sin tocar  
![](https://www.filepicker.io/api/file/MHESJvuGRT6RDHgfv0cI)

* Hablaremos más en detalle del componente *Rigidbody* más adelante.  


---


### Tema 1: Repaso

### 1.4.- Movimiento básico


## Movimiento con el Rigidbody

* Primero debemos obtener una *referencia* a componente desde nuestro script:
  ```cs
Rigidbody rb = this.GetComponent<Rigidbody>();
``` 
* `Getcomponent<T>()` es una función heredada de `MonoBehaviour` que sirve para obtener una referencia a un componente de tipo `<T>`.
* La función busca el componente en el gameobject del objeto desde el que se lanza.<br>
(en este caso el gameobject de `this` que es el player)
* Si encuentra un componente de ese tipo devuelve una referencia al mismo que podemos almacenar en una variable para después acceder a sus propiedades.
* Lo vamos a utilizar para decirle al motor de física con que velocidad ha de moverse el *player* en cada fotograma.
  ```cs
	public float speed;
	private Rigidbody rb;
	
	void Start () {
		this.rb = this.GetComponent<Rigidbody>();
	}
	
	void Update () {
		Vector3 direction = Vector3.forward;
		this.rb.velocity = direction * speed;
	}
	```



----



### Tema 1: Repaso

### 1.5.- User Input I


## Control del movimiento por parte del usuario



La clase `Input` nos da la info de input de usuario (teclado, ratón, controlador consola, ...).
*  ¿Dónde se configura? En el panel *InputManager*:
*  *Edit* 
	*  *Project Settings*
	 	*  *Input*
*  Los ejes llamados `Horizontal` y `Vertical` son los estándar de movimiento en cualquier juego y son los que usaremos.
*  Por defecto vienen configurados para <kbd>W</kbd> <kbd>A</kbd> <kbd>S</kbd> <kbd>D</kbd> y 
<kbd>⇦</kbd> <kbd>⇧</kbd> <kbd>⇩</kbd> <kbd>⇨</kbd>
<br><br>
*  Desde nuestro código:
	* `Input.GetAxisRaw("Horizontal")`
	 	* Nos da el valor del input según la tecla pulsada 
			* <kbd>A</kbd> -> `-1` -> izquierda 
			* <kbd>D</kbd> -> `1` -> derecha
			* ninguna -> `0`
	* `Input.GetAxis("Horizontal")` nos da el valor del input horizontal modificado por los valores de *Sensitivity* y *Gravity* del *InputManager*.


---


### Tema 1: Repaso

### 1.5.- User Input I


## Control Fino del movimiento

*  Configurando las propiedades *Sensitivity* y *Gravity* en *InputManager*:
*  *Edit* 
	*  *Project Settings*
	 	*  *Input*

![](https://www.filepicker.io/api/file/29fIrVi2QgOv8dGTuA8d)


**Sensitivity** controla lo rápido que se alcanza el máximo en ese sentido de movimiento. 
**Gravity** controla cómo de rápido vuelve el valor del eje a 0.  


---


### Tema 1: Repaso

### 1.5.- User Input I


### Versión final del script de movimiento
```cs
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Movement related properties
	public float speed;
	private Rigidbody rb;


	// Initialization function. Executes just once when the GameObject is created at runtime.
	void Start () {
		// Initialize the rigidbody rb property
		this.rb = this.GetComponent<Rigidbody>();
	}

	void Update () {
		// Calculate the movement direction from the Unity input axes.
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical");
		Vector3 direction = zInput * Vector3.forward + xInput * Vector3.right;   

		// Only move the player if there is an input
		if (direction != Vector3.zero) {
			// Note that we use the rigidbody to move the player, and only 
			// when there is input, so we do not interfere with the Physics engine
			this.rb.velocity = direction.normalized * speed;
		}
	}
}
```

