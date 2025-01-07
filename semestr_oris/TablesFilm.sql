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
    description TEXT,
    release_year INT,
    rating FLOAT,
    imdb_rating FLOAT,
    amediateka_rating FLOAT,
    poster_url VARCHAR(255),
    card_url VARCHAR(255)
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

CREATE TABLE staff (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,     
    role VARCHAR(50) NOT NULL,                
);

CREATE TABLE movie_staff (
    movie_id INT NOT NULL,           
    staff_id INT NOT NULL,           
    PRIMARY KEY (movie_id, staff_id),
    FOREIGN KEY (movie_id) REFERENCES movies(id) ON DELETE CASCADE,
    FOREIGN KEY (staff_id) REFERENCES staff(id) ON DELETE CASCADE
);