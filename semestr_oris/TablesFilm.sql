CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    is_admin varchar(1) NOT NUll,
);

CREATE TABLE movies (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description_card NTEXT,
    description NTEXT,
    release_year INT,
    rating FLOAT,
    imdb_rating FLOAT,
    amediateka_rating FLOAT,
    poster_url NVARCHAR(MAX),
    bg_url NVARCHAR(MAX),
    card_url NVARCHAR(MAX)
);

CREATE TABLE genres (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL       
);

CREATE TABLE movie_genres (
    movie_id INT NOT NULL,           
    genre_id INT NOT NULL,           
    PRIMARY KEY (movie_id, genre_id),
    FOREIGN KEY (movie_id) REFERENCES movies(id) ON DELETE CASCADE,
    FOREIGN KEY (genre_id) REFERENCES genres(id) ON DELETE CASCADE
);

CREATE TABLE actors (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Уникальный идентификатор сотрудника
    name VARCHAR(100) NOT NULL         -- Имя сотрудника
);

CREATE TABLE directors (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Уникальный идентификатор сотрудника
    name VARCHAR(100) NOT NULL         -- Имя сотрудника
);

CREATE TABLE movie_staff (
    movie_id INT NOT NULL,          -- Ссылка на фильм
    actor_id INT NULL,              -- Ссылка на актера (может быть NULL, если запись только для режиссера)
    director_id INT NULL,           -- Ссылка на режиссера (может быть NULL, если запись только для актера)
    PRIMARY KEY (movie_id, actor_id, director_id),  -- Уникальная комбинация фильма, актера и режиссера
    FOREIGN KEY (movie_id) REFERENCES movies(id) ON DELETE CASCADE, -- Связь с таблицей фильмов
    FOREIGN KEY (actor_id) REFERENCES actors(id) ON DELETE CASCADE, -- Связь с таблицей актеров
    FOREIGN KEY (director_id) REFERENCES directors(id) ON DELETE CASCADE -- Связь с таблицей режиссеров
);