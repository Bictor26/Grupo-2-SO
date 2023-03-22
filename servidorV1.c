#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>


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
	
	
	
	
	int i;
	
	for(;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexi?n\n");
		//sock_conn es el socket que usaremos para este cliente
		
		//Bucle atención al cliente
		int terminar =0;
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
			
			
			if (codigo !=0){
				p = strtok( NULL, "/");
				strcpy (nombre, p);
				p = strtok( NULL, "/");
				strcpy (contrasena, p);
				printf ("Codigo: %d, Nombre: %s, Contrasena: %s\n", codigo, nombre, contrasena);
			}
			
			
			if (codigo==0)
				terminar=1;
			
			else if (codigo ==1) // Jugador con más puntos
			{
				sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
				
				
				err=mysql_query (conn, consulta1);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					
					
					exit (1);
				}
				
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
					noregistrado=1;
					strcpy (error, "No estas registrado");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
				}
				else
					printf ("El id de %s es: %s\n",nombre, row [0] );
				
				strcpy (consulta2,"SELECT Jugadores.nombre FROM Participaciones, Jugadores WHERE Participaciones.puntos = (SELECT MAX(Participaciones.puntos) FROM Participaciones) AND Participaciones.idJ = Jugadores.id");
				
				err=mysql_query (conn, consulta2);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta
				resultado = mysql_store_result (conn); 
				// Ahora obtenemos la primera fila
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
					
				
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
					
					
				mysql_close (conn);
				exit(0);
				
			}
			else if (codigo ==2){
				sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
				
				
				err=mysql_query (conn, consulta1);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					
					
					exit (1);
				}
				
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
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
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta
				resultado = mysql_store_result (conn); 
				// Ahora obtenemos la primera fila
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				
				
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
				
				
				mysql_close (conn);
				exit(0);
				
				
				
			}
			else if (codigo ==3){
				
				sprintf (consulta1,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
				
				
				err=mysql_query (conn, consulta1);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					
					
					exit (1);
				}
				
				resultado = mysql_store_result(conn);
				
				row = mysql_fetch_row (resultado);
				
				
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
					noregistrado=1;
					strcpy (error, "No estas registrado");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
				}
				else
					printf ("El id de %s es: %s\n",nombre, row [0] );
				
				strcpy (consulta2,"SELECT Partidas.tiempo FROM Partidas, Participaciones, Jugadores WHERE Partidas.id = Participaciones.idP AND Participaciones.idj = Jugadores.id AND Jugadores.nombre = 'Juan'");
				
				err=mysql_query (conn, consulta2);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta
				resultado = mysql_store_result (conn); 
				// Ahora obtenemos la primera fila
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				
				
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
				
				
				mysql_close (conn);
				exit(0);
				
			}
			else if (codigo ==4){
				
				
				sprintf (consulta4, "SELECT Jugadores.nombre FROM Jugadores WHERE Jugadores.nombre = '%s'",nombre);
				
				err=mysql_query (conn, consulta4);
				if (err==0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					
					strcpy (error, "El nombre de usuario ya existe");
					// Enviamos la respuesta
					write (sock_conn,error, strlen(error));
					
					exit (1);
				}
				else{
					int id;
					sprintf (consulta1, "SELECT Jugadores.id FROM Jugadores WHERE Jugadores.id = (SELECT MAX(Jugadores.id) FROM Jugadores)");
					
					err=mysql_query (conn, consulta1);
					if (err!=0) {
						printf ("Error al consultar datos de la base %u %s\n",
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
						printf ("Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						
						
						
						exit (1);
					}
					
					sprintf (consulta3,"SELECT Jugadores.id FROM Jugadores WHERE Jugadores.nombre = '%s' AND Jugadores.contrasena = '%s'",nombre, contrasena );
					
					
					err=mysql_query (conn, consulta3);
					if (err!=0) {
						printf ("Error al consultar datos de la base %u %s\n",
								mysql_errno(conn), mysql_error(conn));
						
						
						
						exit (1);
					}
					
					resultado = mysql_store_result(conn);
					
					row = mysql_fetch_row (resultado);
					
					
					if (row == NULL)
					{
						printf ("No se han obtenido datos en la consulta\n");
						noregistrado=1;
						
						strcpy (error, "No estas registrado correctamente");
						// Enviamos la respuesta
						write (sock_conn,error, strlen(error));
					}
					else{
						
						strcpy (error, "Ya estas registrado!");
						// Enviamos la respuesta
						write (sock_conn,error, strlen(error));
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
	
}
