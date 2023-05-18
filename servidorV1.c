#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex =PTHREAD_MUTEX_INITIALIZER;

//Estructura de datos para almacenar 100 conectados
typedef struct{
	char nombre[20];
	int socket;
	
}Conectado;

typedef struct{
	Conectado conectados[100];
	int num;
}ListaConectados;

typedef struct{
	char nombre1[20];
	int socket1;
	int aceptado1;
	
	char nombre2[20];
	int socket2;
	int aceptado2;
	
	char nombre3[20];
	int socket3;
	int aceptado3;
	
	char nombre4[20];
	int socket4;
	int aceptado4;
	
}Partida;

typedef Partida TablaPartidas[100];

TablaPartidas tabla;

int NumPartida(TablaPartidas tabla){
	int result = strlen(tabla);
	return result;
}
int QueJugador(TablaPartidas tabla, int id, char nombre[20]){
	int result;
	if (strcmp(tabla[id].nombre2, nombre)==0){
		result = 2;
		return result;
	}
	else if (strcmp(tabla[id].nombre3, nombre)==0){
		result = 3;
		return result;
	}
	else if (strcmp(tabla[id].nombre4, nombre)==0){
		result = 4;
		return result;
	}
	
}
void ActualizarAceptado(TablaPartidas tabla,int id, char nombre[20], int aceptado ){
	
	int quejugador = QueJugador(tabla,id,nombre);
	
	if (aceptado==1){
		
		if(quejugador==2){
			tabla[id].aceptado2 = 1;
		}
		else if (quejugador==3){
			tabla[id].aceptado3 = 1;
		}
		else if (quejugador==4){
			tabla[id].aceptado4 = 1;
		}
	}
	else if (aceptado==2){
		
		if(quejugador==2){
			tabla[id].aceptado2 = 2;
		}
		else if (quejugador==3){
			tabla[id].aceptado3 = 2;
		}
		else if (quejugador==4){
			tabla[id].aceptado4 = 2;
		}
	}
	
}
//aceptado=1 acepta solicitud de juego 
//aceptado=2 no acepta solicitud de juego
//aceptado=0 neutro
void Inicializar (TablaPartidas tabla){
	int i;
	int a = NumPartida(tabla); 
	
	for (i=0;i<a;i++){
		tabla[i].aceptado1=0;
		tabla[i].aceptado2=0;
		tabla[i].aceptado3=0;
		tabla[i].aceptado4=0;
		
		tabla[i].socket1=0;
		tabla[i].socket2=0;
		tabla[i].socket3=0;
		tabla[i].socket4=0;
	}
}

ListaConectados lista;
char conectados[300];

int i;
int sockets[100];

int AnadirConectado(char nombre[20], int socket, ListaConectados *lista)
{
	
	if (lista->num==100)
		return -1;
	else{
		strcpy(lista->conectados[lista->num].nombre,nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
	
}


int DameSocket(ListaConectados *lista, char nombre[20])
{
	int j = 0;
	int encontrado = 0;
	while (encontrado == 0 && lista->num != j){
		if(strcmp(lista->conectados[j].nombre,nombre) == 0)
			encontrado = 1;
		else
			j = j + 1;
	}
	if (encontrado == 0)
		return -1;
	else
		return lista->conectados[j].socket;
}

int DamePosicion(char nombre[20],ListaConectados *lista)
{
	
	int j = 0;
	int encontrado = 0;
	while ((encontrado == 0) && (lista->num != j)){
		if(strcmp(lista->conectados[j].nombre,nombre) == 0)
			encontrado = 1;
		else
			j = j + 1;
	}
	if (encontrado == 0)
		return -1;
	else
		return j;
}

int EliminarConectado(char nombre[20],ListaConectados *lista)
{
	int p = DamePosicion(nombre,lista);
	
	if(p == -1)
		return -1;
	else{
		int i;
		for(i = p; i < lista -> num; i++){
			lista->conectados[i].socket = lista->conectados[i+1].socket;
			strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);
			lista->num = lista->num -1;
		}
		return 0;
		
	}
}

void DameConectados(ListaConectados *lista, char result[200])
{
	sprintf(result,"%d/",lista->num);
	int i;
	for(i = 0; i < lista->num; i++){
		sprintf(result,"%s%s/",result,lista->conectados[i].nombre);
	}
}

void *AtenderUsuario (void *socket)
{
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn=*s;
	
	char peticion[512];
	char respuesta[512];
	
	int ret;
	
	//Realizamos conexion con mysql
	
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	char consulta1[200];
	char consulta2[200];
	char consulta3 [200];
	char consulta4 [200];
	char error[50];
	int noregistrado=0;
	char notificacion[200];
	char ListaConcetados[200];
	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "LaserTails",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	

	int terminar = 0;
	while (terminar==0){
		// Ahora recibimos su peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibida una petición\n");
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		//Escribimos la peticion en la consola
		
		printf ("La petición es: %s\n",peticion);
		char *p = strtok(peticion, "/");
		int codigo =  atoi (p);
		char nombre[20];
		char contrasena[50];
		char nombrepeticion[20];
		
		if (codigo !=0 && codigo !=1 && codigo !=6 && codigo !=7){
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			p = strtok( NULL, "/");
			strcpy (contrasena, p);
			printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", codigo, nombre, contrasena);
		}
		if (codigo==0) {	
			//Al modificar la lista de conectados necesitamos garantizar el acceso excluyente
			pthread_mutex_lock(&mutex);
			int res = EliminarConectado(nombre, &lista);
			pthread_mutex_unlock(&mutex);
			if (res==-1)
				printf("Error al eliminar\n");
			else
				printf("Eliminado correctamente\n");
			
			DameConectados(&lista, ListaConcetados);
			sprintf(notificacion, "5/Desconectado correctamente/%s",ListaConcetados);
			printf("Resultado:'%s'\n", notificacion);
			
			int j;
			for (j=0; j< lista.num; j++)
				write(lista.conectados[j].socket,notificacion, strlen(notificacion));
			terminar=1;
		}
		else if (codigo ==1){	// Jugador con más puntos
			strcpy (consulta2,"SELECT Jugadores.nombre FROM Participaciones, Jugadores WHERE Participaciones.puntos = (SELECT MAX(Participaciones.puntos) FROM Participaciones) AND Participaciones.idJ = Jugadores.id");
			
			err=mysql_query (conn, consulta2);
			if (err!=0) {
				printf ("1/Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"1/Error en la consulta");
				write (sock_conn,respuesta, strlen(respuesta));
			} else {
				//recogemos el resultado de la consulta
				resultado = mysql_store_result (conn);
				// Ahora obtenemos la primera fila
				row = mysql_fetch_row (resultado);
				
				if (row == NULL) {
					printf ("1/No se han obtenido datos en la consulta\n");
					strcpy(respuesta,"1/Error en la consulta");
					write (sock_conn,respuesta, strlen(respuesta));
				}
				else {
					printf ("El jugador con record de puntos es: %s\n", row [0] );
					sprintf (resultado,"1/El jugador con record de puntos es: %s\n", row [0] );
					strcpy(respuesta,resultado);
					printf ("Respuesta: %s\n", respuesta);
					// Enviamos la respuesta
					write (sock_conn,respuesta, strlen(respuesta));
				}
			}
		}
		else if (codigo ==2){	//color de Roberta en la partida 3
			sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
			
			
			err=mysql_query (conn, consulta1);
			if (err!=0) {
				printf ("2/Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				
				
				exit (1);
			}
			
			resultado = mysql_store_result(conn);
			
			row = mysql_fetch_row (resultado);
			
			
			if (row == NULL)
			{
				printf ("2/No se han obtenido datos en la consulta\n");
				noregistrado=1;
				
				strcpy (error, "No estas registrado");
				// Enviamos la respuesta
				write (sock_conn,error, strlen(error));
			}
			else
				printf ("El id de %s es: %s\n",nombre, row [0] );
			
			strcpy (consulta2,"SELECT Participaciones.color FROM Jugadores, Participaciones WHERE Jugadores.nombre = 'Roberta' AND Jugadores.id = Participaciones.idJ AND Participaciones.idP = 3");
			
			err=mysql_query (conn, consulta2);
			if (err!=0) {
				printf ("2/Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta
			resultado = mysql_store_result (conn); 
			// Ahora obtenemos la primera fila
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
				printf ("2/No se han obtenido datos en la consulta\n");
			
			
			else
				printf ("El color de Roberta en la partida 3 es: %s\n", row [0] );
			sprintf (resultado,"2/El color de Roberta en la partida 3 es: %s\n", row [0] );
			strcpy(respuesta,resultado);
			
			if (noregistrado==0)
			{
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos la respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
		}
		else if (codigo ==3){	//tiempo de la partida de Juan
			
			sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
			
			
			err=mysql_query (conn, consulta1);
			if (err!=0) {
				printf ("3/Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result(conn);
			
			row = mysql_fetch_row (resultado);
			
			
			if (row == NULL)
			{
				printf ("3/No se han obtenido datos en la consulta\n");
				noregistrado=1;
				strcpy (error, "No estas registrado");
				// Enviamos la respuesta
				write (sock_conn,error, strlen(error));
			}
			else
				printf ("3/El id de %s es: %s\n",nombre, row [0] );
			
			strcpy (consulta2,"SELECT Partidas.tiempo FROM Partidas, Participaciones, Jugadores WHERE Partidas.id = Participaciones.idP AND Participaciones.idj = Jugadores.id AND Jugadores.nombre = 'Juan'");
			
			err=mysql_query (conn, consulta2);
			if (err!=0) {
				printf ("3/Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta
			resultado = mysql_store_result (conn); 
			// Ahora obtenemos la primera fila
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
				printf ("3/No se han obtenido datos en la consulta\n");
			
			
			else
				printf ("El tiempo de la partida de Juan fue: %s min.\n", row [0] );
				sprintf (resultado,"3/El tiempo de la partida de Juan fue: %s min.\n", row [0] );
				strcpy(respuesta,resultado);
			
			if (noregistrado==0)
			{
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos la respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
		}
		else if (codigo ==4){	//Registro del ususario
			sprintf (consulta, "SELECT Jugadores.nombre FROM Jugadores WHERE Jugadores.nombre = '%s'",nombre);
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("4/Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"4/Error en la consulta");
				write (sock_conn,respuesta, strlen(respuesta));
			} else {
				resultado = mysql_store_result(conn);				
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{
					printf ("4/El jugador se puede registrar\n");
					sprintf (consulta2,"INSERT INTO Jugadores (nombre, contrasena) VALUES ('%s', '%s');", nombre, contrasena);
					err=mysql_query (conn, consulta2);
					
					if (err!=0) {
						printf ("4/Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
						strcpy(respuesta,"4/Error en la consulta");
						write (sock_conn,respuesta, strlen(respuesta));
					} else {
						printf ("4/Jugador Registrado.");
						strcpy(respuesta,"4/Registrado correctamente");
						write (sock_conn,respuesta, strlen(respuesta));
					}
				}
				else{
					printf ("4/El jugador ya estaba registrado\n");
					strcpy(respuesta,"4/Ya estabas registrado, inicia sesion!");
					write (sock_conn,respuesta, strlen(respuesta));
				}
			}
		}
		else if (codigo ==5){	//Inicio de sesión y conexión al servidor
			sprintf (consulta, "SELECT Jugadores.nombre FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena);
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("5/Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"5/Error en la consulta");
				write (sock_conn,respuesta, strlen(respuesta));
			} 
			else {
				resultado = mysql_store_result(conn);
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{//El jugador no existe, se tiene que registrar
					printf ("5/No se han obtenido datos en la consulta\n");
					strcpy(respuesta,"5/No estas registrado");
					write (sock_conn,respuesta, strlen(respuesta));
				}
				else {
					//Al modificar la lista de conectados necesitamos garantizar el acceso excluyente
					pthread_mutex_lock(&mutex);
					int res = AnadirConectado(nombre, sock_conn, &lista);
					pthread_mutex_unlock(&mutex);
					if (res==-1) {
						printf("Lista llena\n");
						strcpy(respuesta,"5/Maximo de jugadores conectados.");
						write (sock_conn,respuesta, strlen(respuesta));
					} 
					else {
						printf("Añadido bien\n");
						int socket= DameSocket(&lista, nombre);
						printf("El socket de '%s' es: %d\n", nombre, socket);
						
						DameConectados(&lista, ListaConcetados);
						sprintf(notificacion, "5/Conectado correctamente/%s",ListaConcetados);
						printf("Resultado:'%s'\n", notificacion);
						
						int j;
						for (j=0; j< lista.num; j++)
							write(lista.conectados[j].socket,notificacion, strlen(notificacion));
					}
				}
			}
		}
		else if (codigo==6){	//Partida creada por el anfitrión
			
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			printf("Resultado:'%s'\n", nombre);
			
			int numpartida = NumPartida(tabla);
			
			strcpy(tabla[numpartida].nombre1, nombre);
			tabla[numpartida].socket1 = sock_conn;
			tabla[numpartida].aceptado1 = 1;
			
			sprintf (notificacion, "6/%s/%d",nombre,numpartida);
			
			p = strtok( NULL, "/");
			strcpy (nombrepeticion, p);
			
			if (nombrepeticion != NULL){
				
				strcpy(tabla[numpartida].nombre2, nombrepeticion);
				
				for(int p=0; p<lista.num; p++){
					
					if (strcmp(lista.conectados[p].nombre , nombrepeticion)==0){
						tabla[numpartida].socket2 = lista.conectados[p].socket;
						tabla[numpartida].aceptado2 = 2;
					}
				}
				// Enviamos la respuesta
				write (tabla[numpartida].socket2,notificacion, strlen(notificacion));
				p = strtok( NULL, "/");
			}
			
			if (p != NULL){
				strcpy (nombrepeticion, p);
				
				strcpy(tabla[numpartida].nombre3, nombrepeticion);
				
				for(int p=0; p<lista.num; p++){
					
					if (strcmp(lista.conectados[p].nombre , nombrepeticion)==0){
						tabla[numpartida].socket3 = lista.conectados[p].socket;
						tabla[numpartida].aceptado3 = 2;
					}
				}
				// Enviamos la respuesta
				write (tabla[numpartida].socket3,notificacion, strlen(notificacion));
				p = strtok( NULL, "/");
			}
			
			if (p != NULL){
				strcpy (nombrepeticion, p);
				printf("Resultado:'%s'\n", nombrepeticion);
				strcpy(tabla[numpartida].nombre4, nombrepeticion);
				
				for(int p=0; p<lista.num; p++){
					
					if (strcmp(lista.conectados[p].nombre , nombrepeticion)==0){
						tabla[numpartida].socket4 = lista.conectados[p].socket;
						tabla[numpartida].aceptado4 = 2;
					}
				}
				// Enviamos la respuesta
				write (tabla[numpartida].socket4,notificacion, strlen(notificacion));
			}		
		}
		else if (codigo==7){	//Respuesta de jugador y notificación a anfitrión
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			printf("Resultado:'%s'\n", nombre);
			p = strtok( NULL, "/");
			int id = atoi(p);
			p = strtok( NULL, "/");
			int aceptado = atoi(p);
			
			ActualizarAceptado(tabla, id, nombre, aceptado);
			
			sprintf (notificacion, "7/%s/%d",nombre,aceptado);
			// Enviamos la respuesta
			write (tabla[id].socket1,notificacion, strlen(notificacion));
			
			if (aceptado == 2) {
				printf("Aceptado2: %i-%i-%i-%i\n", tabla[id].aceptado1, tabla[id].aceptado2, tabla[id].aceptado3, tabla[id].aceptado4);
				sprintf (notificacion, "8/0");
				
				if (tabla[id].aceptado1 == 1 || tabla[id].aceptado1 == 2) {
					write (tabla[id].socket1,notificacion, strlen(notificacion));
				}
				if (tabla[id].aceptado2 == 1 || tabla[id].aceptado2 == 2) {
					write (tabla[id].socket2,notificacion, strlen(notificacion));
				}
				if (tabla[id].aceptado3 == 1 || tabla[id].aceptado3 == 2) {
					write (tabla[id].socket3,notificacion, strlen(notificacion));
				}
				if (tabla[id].aceptado4 == 1 || tabla[id].aceptado4 == 2) {
					write (tabla[id].socket4,notificacion, strlen(notificacion));
				}
			} else {
				if ((tabla[id].aceptado1 == 1 || tabla[id].aceptado1 == NULL) &&
					(tabla[id].aceptado2 == 1 || tabla[id].aceptado2 == NULL) &&
					(tabla[id].aceptado3 == 1 || tabla[id].aceptado3 == NULL) &&
					(tabla[id].aceptado4 == 1 || tabla[id].aceptado4 == NULL)) {
					printf("Aceptado1: %i-%i-%i-%i\n", tabla[id].aceptado1, tabla[id].aceptado2, tabla[id].aceptado3, tabla[id].aceptado4);
					sprintf (notificacion, "8/1");
					if (tabla[id].aceptado1 == 1)
						write (tabla[id].socket1,notificacion, strlen(notificacion));
					if (tabla[id].aceptado2 == 1)
						write (tabla[id].socket2,notificacion, strlen(notificacion));
					if (tabla[id].aceptado3 == 1)
						write (tabla[id].socket3,notificacion, strlen(notificacion));
					if (tabla[id].aceptado4 == 1)
						write (tabla[id].socket4,notificacion, strlen(notificacion));
				}
			}
		}
		else{
			strcpy (error, "Selecciona una opcion");
			// Enviamos la respuesta
			write (sock_conn,error, strlen(error));
		}
		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
}
int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9050
	serv_adr.sin_port = htons(9000);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 4) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread;
	i=0;
	for(;;)
	{
		printf ("Escuchando\n");
					   
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexi?n\n");
		printf("%i",sock_conn);
		sockets[i] = sock_conn;
		//sock_conn es el socket que usaremos para este cliente
					   
		//Bucle atención al cliente
		pthread_create (&thread, NULL, AtenderUsuario, &sockets[i]);
		i++;
	}
}
