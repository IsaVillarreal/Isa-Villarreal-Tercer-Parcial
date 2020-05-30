CREATE TABLE Playlistss
(
	
	playlist_id int,
	nombre_playlist text,
	cancion_nom text,
	artista_nom text,
	album_cancion text,
	yyyy_cancion int,
	fecha_playlist date,
	Primary key(nombre_playlist));
	
	select * from Playlistss;
	
	INSERT INTO Playlistss (playlist_id, nombre_playlist, cancion_nom, artista_nom,album_cancion,yyyy_cancion,fecha_playlist)
VALUES (1, 'Melancolia', 'Me dedique a perderte', 'Alejandro Fernandez', 'A corazon abierto',2004, todate(now()));	
INSERT INTO Playlistss (playlist_id, nombre_playlist, cancion_nom, artista_nom,album_cancion,yyyy_cancion,fecha_playlist)
VALUES (2, 'Clasicos', 'Mi primer millon', 'Bacilos', 'Grandes Exitos',2006, todate(now()));
INSERT INTO Playlistss (playlist_id, nombre_playlist, cancion_nom, artista_nom,album_cancion,yyyy_cancion,fecha_playlist)
VALUES (3, 'Latina', 'La gozadera', 'Gente de zona', 'Visualizate',2015, todate(now()));
INSERT INTO Playlistss (playlist_id, nombre_playlist, cancion_nom, artista_nom,album_cancion,yyyy_cancion,fecha_playlist)
VALUES (4, 'Alegria', 'Que tire pa lante', 'Daddy Yankee', 'Que tire pa lante',2019, todate(now()));
			
	


			
	

