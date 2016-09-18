
#### <!-- .element: class="title" --> Tema 0
# <!-- .element: class="title" --> Repaso y refuerzo de programación en C# con Unity



----


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.0.- Mecánicas de juego. 
<!-- .element: class="head-right" -->

## Un paseo por las mecánicas del juego

  * <!-- .element: class="fragment" --> Movimiento del player.
  * <!-- .element: class="fragment" --> Caida (no hay salto).
  * <!-- .element: class="fragment" --> Switches (interruptores) y sus acciones.
  * <!-- .element: class="fragment" --> Inventario (De momento sólo coge piedras).
  * <!-- .element: class="fragment" --> Cámara (seguimiento y rotación).
  * <!-- .element: class="fragment" --> Fin de nivel.
  

----


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.1.-Preparar el proyecto
<!-- .element: class="head-right" -->

## Organización del proyecto
 
Podéis descargar el proyecto inicial [aquí](https://github.com/glantucan/puzzle_game/archive/00_BareProject.zip).

* <!-- .element: class="fragment" --> Colocación ventanas de Unity (y guardado). 
* <!-- .element: class="fragment" --> Nombres de carpetas y archivos en `Assets`.  
* <!-- .element: class="fragment" --> GameObjects anidados para organizar la jerarquía de escena
* <!-- .element: class="fragment" --> Configuración para trabajar con MonoDevelop.
  * Descarga [este archivo](https://drive.google.com/open?id=0B97mmF9E10p3SUVCQm9yXy03YXM).
  * Descomprímelo en una carpeta temporal.
  * Copia las carpetas que hay dentro a  
  `c:\\Users\your_user_name\AppData\Roaming\MonoDevelop-Unity-5.0`.  
Borra lo que haya dentro de esa carpeta previamente.


----


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.2.- GameObjects y Componentes
<!-- .element: class="head-right" -->

## Recordatorio básico
* <!-- .element: class="fragment" --> **GameObject**: Pieza básica para construir elementos de juego
* <!-- .element: class="fragment" --> **Componente**: Configura el comportamiento y aspecto del GameObject.
	* <!-- .element: class="fragment" --> Componente `Transfrom` (posición, rotación y escala)
  	* <!-- .element: class="fragment" --> Otros componentes típicos  
		* `MeshFilter` -> Definición de la malla (Vértices y aristas)
		* `MeshRenderer` -> material: texturas, color, shaders...
		* `Collider` -> *Solidez* del game object.
		* `Rigidbody` -> Propiedades *físicas* del gameobject
		* ...
		* <!-- .element: class="fragment" --> Scripts (que heredan las propiedades de `MonoBehaviour`)  
		Cuando no haya un componente prefabricado que haga lo que queremos, lo creamos nosotros en C#.  
<br>  

### <!-- .element: class="fragment" --> Nuestro juego tendrá inicialmente:
* <!-- .element: class="fragment" --> Muchos gameobjects
* <!-- .element: class="fragment" --> Unos cuantos componentes en cada gameobject &rArr; Muchísimos componentes
* <!-- .element: class="fragment" --> Un sólo compnente de tipo Script (`Player.cs`)


----


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->

### Primero vamos a hacer las cosas mal. 
#### &rArr; Para que entendáis por qué hay que hacerlas bien. <!-- .element: class="fragment" --> 
* <!-- .element: class="fragment" --> Vamos a meter todas las mecánicas del juego en un sólo componente.  
* <!-- .element: class="fragment" --> Acabaremos con un script de más de 300 líneas
  * Veremos que nos falta mucho por hacer para acabar el juego
  * Nos daremos cuenta de que es prácticamente imposible orientarse entre tanto código.
  * Hacer cambios en una mecánica afecta al código de las otras. Modificar un pequeño detalle desencadena cascadas de cambios que no acaban nunca.

<p></p>



---


<!-- .element: class="cols2" -->

### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->

### Después haremos las cosas regular
* <!-- .element: class="fragment arrow no-margin" --> 
Para organizar nuestro código y nuestra cabeza 
	* Separaremos cada pieza de las mecánicas en funciones diferentes.
	* Cada función debe funcionar como una caja negra.
		* La ejecuto, le doy la info que necesita.
		* Hace **sólo** su tarea.
		* Me devuelve el control.
			* Puedo hacer que me devuelva información útil si lo necesito.



### Y después haremos las cosas bien 
* <!-- .element: class="fragment arrow no-margin" -->  Divide y vencerás: 
	* Cada pieza del juego va en un componente diferente.
	* Cada pieza es lo más independiente de las otras que se pueda.
		* Veremos que todo es mucho más claro y manejable
		* Podremos cambiar y añadir cosas sin cambiar código de otras piezas.
	  
  


---


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->

### Creando el componente `Player`
1. Crear un nuevo Script
	* <!-- .element class="arrow" --> En la carpeta *Scripts* de nuestros *Assets* 
		* <!-- .element class="arrow" --> Click con el botón derecho 
			* <!-- .element class="arrow" --> *Create* 
				* <!-- .element class="arrow" --> *C# Script*  
					* <!-- .element class="arrow" --> Ponemos `Player` como nombre  
					
	El nombre de la clase coincide con el nombre del archivo.   
	**Esto debe cumplirse siempre**.  
	Si no el script no funcionará.  
4. Arrastrad el script desde *Assets/Scripts* al gameobject *Player* 


---


### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->


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
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->

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
<!-- .element: class="head-left" -->
### 0.3.- Scripts y MonoBehaviour
<!-- .element: class="head-right" -->

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



<!-- .element: class="cols2" -->

### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.4.- Movimiento básico
<!-- .element: class="head-right" -->


## <!-- .element: class="span2" --> Dos formas de mover un GameObject (Con velocidad constante) <br>
<!-- .element: class="span2" --> <button class="warning">WARNING!</button> A partir de ahora necesitas entender como operar con vectores.

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
<!-- .element: class="span2" --> El vector `direction` debe tener longitud 1 para que `moveVel` actúe como esperamos.

<!-- .element: class="span2" --> Podemos utilizar `direction.normalized` en lugar de `direction` si no lo es.


Note: 
* Stop here and show some examples. Make them do some exercises before going further.



----



### Tema 0: Repaso
<!-- .element: class="head-left" -->
### 0.5.- User Input I
<!-- .element: class="head-right" -->

## Control del movimiento por parte del usuario



La clase `Input` nos da la info de input de usuario (teclado, ratón, controlador consola, ...).
* <!-- .element class="no-bullet" --> ¿Dónde se configura? En el panel *InputManager*:
* <!-- .element class="arrow" --> *Edit* 
	* <!-- .element class="arrow" --> *Project Settings*
	 	* <!-- .element class="arrow" --> *Input*
* <!-- .element class="no-bullet" --> Los ejes llamados `Horizontal` y `Vertical` son los estándar de movimiento en cualquier juego y son los que usaremos.
* <!-- .element class="no-bullet" --> Por defecto vienen configurados para <kbd>W</kbd> <kbd>A</kbd> <kbd>S</kbd> <kbd>D</kbd> y 
<kbd>⇦</kbd> <kbd>⇧</kbd> <kbd>⇩</kbd> <kbd>⇨</kbd>
<br><br>
* <!-- .element class="no-bullet" --> Desde nuestro código:
	* `Input.GetAxisRaw("Horizontal")`
	 	* Nos da el valor del input según la tecla pulsada 
			* <kbd>A</kbd> -> `-1` -> izquierda 
			* <kbd>D</kbd> -> `1` -> derecha
			* ninguna -> `0`
	* `Input.GetAxis("Horizontal")` nos da el valor del input horizontal modificado por los valores de *Sensitivity* y *Gravity* del *InputManager*.


