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

ListaConectados lista;
char conectados[300];

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
	
	int AnadirConectado(char nombre[20], int socket, ListaConectados *lista)
	{
	
		if (lista->num==100)
			return -1;
		else{
			//Al modificar la lista de conectados necesitamos garantizar el acceso excluyente
			pthread_mutex_lock(&mutex);
			strcpy(lista->conectados[lista->num].nombre,nombre);
			lista->conectados[lista->num].socket = socket;
			lista->num++;
			pthread_mutex_unlock(&mutex);
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
				pthread_mutex_lock(&mutex);
				lista->conectados[i].socket = lista->conectados[i+1].socket;
				strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);
				lista->num = lista->num -1;
				pthread_mutex_unlock(&mutex);
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
	
	int i;
	
	for(;;){

		int terminar =0;
		while (terminar==0){
			
			// Ahora recibimos su peticion
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibida una petici�n\n");
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			//Escribimos la peticion en la consola
			
			printf ("La petici�n es: %s\n",peticion);
			char *p = strtok(peticion, "/");
			int codigo =  atoi (p);
			char nombre[20];
			char contrasena[50];
			
			
			if (codigo !=0){
				p = strtok( NULL, "/");
				strcpy (nombre, p);
				p = strtok( NULL, "/");
				strcpy (contrasena, p);
				printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", codigo, nombre, contrasena);
			}
			
			
			if (codigo==0)
			{	
				int res =EliminarConectado(nombre, &lista);
				if (res==-1)
					printf("Error al eliminar\n");
				else
					printf("Eliminado correctamente\n");
				
				terminar=1;
			}
			else if (codigo ==1) // Jugador con m�s puntos
			{
				sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
				
				
				err=mysql_query (conn, consulta1);
				if (err!=0) {
					printf ("1/Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					exit (1);
				}
				
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				
				if (row == NULL)
				{
					printf ("1/No se han obtenido datos en la consulta\n");
					noregistrado=1;
					strcpy (error, "No estas registrado");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
				}
				else
					printf ("1/El id de %s es: %s\n",nombre, row [0] );
				
				strcpy (consulta2,"SELECT Jugadores.nombre FROM Participaciones, Jugadores WHERE Participaciones.puntos = (SELECT MAX(Participaciones.puntos) FROM Participaciones) AND Participaciones.idJ = Jugadores.id");
				
				err=mysql_query (conn, consulta2);
				if (err!=0) {
					printf ("1/Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta
				resultado = mysql_store_result (conn); 
				// Ahora obtenemos la primera fila
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
					printf ("1/No se han obtenido datos en la consulta\n");
				
				
				else
					printf ("El jugador con record de puntos es: %s\n", row [0] );
				sprintf (resultado,"El jugador con record de puntos es: %s\n", row [0] );
				strcpy(respuesta,resultado);
				
				if (noregistrado==0)
				{
					printf ("Respuesta: %s\n", respuesta);
					// Enviamos la respuesta
					write (sock_conn,respuesta, strlen(respuesta));
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
				sprintf (resultado,"El color de Roberta en la partida 3 es: %s\n", row [0] );
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
				sprintf (resultado,"El tiempo de la partida de Juan fue: %s min.\n", row [0] );
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
					printf ("4/Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					exit(1);
				}
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{
					printf ("4/El jugador se puede registrar\n");
					
					strcpy (error, "0");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
					
					int id;
					sprintf (consulta1, "SELECT Jugadores.id FROM Jugadores WHERE Jugadores.id = (SELECT MAX(Jugadores.id) FROM Jugadores)");
					
					err=mysql_query (conn, consulta1);
					if (err!=0) {
						printf ("4/Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						
						exit (1);
					}
					
					resultado = mysql_store_result(conn);
					
					row = mysql_fetch_row (resultado);
					
					id = atoi(row[0]);
					id = id + 1;
					
					sprintf (consulta2,"INSERT INTO Jugadores VALUES (%d,'%s', '%s');", id, nombre, contrasena);
					
					
					err=mysql_query (conn, consulta2);
					if (err!=0) {
						printf ("4/Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						
						exit (1);
					}
					
				}
				else{
					printf ("4/El jugador ya estaba registrado\n");
					
					strcpy (error, "1");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
					
					terminar=1;
				}
				
				terminar=1;
				
			}
			else if (codigo ==5){	//Inicio de sesi�n y conexi�n al servidor
				sprintf (consulta, "SELECT Jugadores.nombre FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena);
				
				err=mysql_query (conn, consulta);
				
				if (err!=0) {
					printf ("5/Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					exit(1);
				}
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{//El jugador no existe, se tiene que registrar
					printf ("5/No se han obtenido datos en la consulta\n");
					
					strcpy (error, "0");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
					
					terminar=1;
				}
				else{
					strcpy (error, "1");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
					
					//Introducir al usuario en la lista de conectados
					int res = AnadirConectado(nombre, socket, &lista);
					if (res==-1)
						printf("Lista llena\n");
					else
						printf("A�adido bien\n");
					
					int socket= DameSocket(&lista, nombre);
					if (socket!=-1)
						printf("El socket de '%s' es: %d\n", nombre, socket);
					else
						printf("Ese usuario no existe\n");
					
					char ListaConcetados[200];
					DameConectados(&lista, ListaConcetados);
					printf("Resultado:'%s'\n", ListaConcetados);
					
				}
			}
			else if (codigo ==6){    //Separa el mensaje de foma correcta 
				char ListaConcetados[200];
				DameConectados(&lista, ListaConcetados);
				write (sock_conn, ListaConcetados, strlen(ListaConcetados));
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
	serv_adr.sin_port = htons(9050);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 4) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	pthread_t thread;
	i=0;
	for(;;)
	{
		printf ("Escuchando\n");
					   
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexi?n\n");
		
		sockets[i] = sock_conn;
		//sock_conn es el socket que usaremos para este cliente
					   
		//Bucle atenci�n al cliente
		pthread_create (&thread, NULL, AtenderUsuario, &sockets[i]);
		i++;
	}
}
