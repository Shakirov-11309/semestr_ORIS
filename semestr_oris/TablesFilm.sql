CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    is_admin int(1) NOT NUll,
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
    card_url NVARCHAR(MAX),
    genre NVARCHAR(MAX),
    country NVARCHAR(MAX),
    url_film VARCHAR(MAX)
);

CREATE TABLE genres (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL       
);

CREATE TABLE countries (
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

CREATE TABLE movie_countries (
    movie_id INT NOT NULL,           
    country_id INT NOT NULL,           
    PRIMARY KEY (movie_id, country),
    FOREIGN KEY (movie_id) REFERENCES movies(id) ON DELETE CASCADE,
    FOREIGN KEY (genre_id) REFERENCES countries(id) ON DELETE CASCADE
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


INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Наму', N'Прекрасная и душераздирающая история художника, жизнь которого разворачивается на холме рядом с деревом, символизирующим его жизнь.', N'Короткометражный анимационный фильм от корейского режиссера Эрика О, номинанта на «Оскар». В центре повествования «Наму» (в переводе с корейского — «дерево») находится максимально личная и трогательная история жизни мужчины-художника – от момента рождения до самой смерти. Вдохновением для этой ленты стала история дедушки режиссера. Действие разворачивается на холме рядом с деревом, которое символизирует жизнь героя: на протяжении многих лет оно впитывало важные воспоминания — от детских игрушек до семейных портретов и других памятных предметов. Вас ждет путешествие по самым прекрасным и душераздирающим моментам человеческой жизни, созданное на стыке классической рисованной анимации и технологий виртуальной реальности.', 2021, 18, 7.3, 7.1, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/1/97/89844fefefdcfa800ef5388c801c3197-220781-3c23c6d1e73947af93a0a1cd0498af26.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/1/97/89844fefefdcfa800ef5388c801c3197-220781-3c23c6d1e73947af93a0a1cd0498af26.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/9/06/b1655b708246a6526d457fa0b191e906-220780-ad0520a527e34fa497ac81efef796e95.png', N'Короткий метр', N'США', 'https://vkvideo.ru/video-220787057_456339168?ref_domain=kino.mail.ru')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'моя соседка-призрак', N'Мальчик встречает дух веселой и отважной девушки 1920-х годов, после чего между ними возникает необычная, но глубокая связь сквозь десятилетия.', N'Проникновенная история паранормальной любви, не знающей границ между мирами. По сюжету фильма «Моя соседка – призрак» подросток по имени Коул (Майкл Чимино, «С любовью, Виктор») переезжает в новую квартиру и встречает духа девушки из 1920-х годов по имени Беа (Пейтон Лист, «Помни меня», «Сплетница»). Она застряла в лимбе из-за магического кольца, которое надела перед тем, как неожиданно погибнуть в аварии. Коул давно перестал петь и радоваться жизни из-за утраты отца, и Беа напомнила ему о силе воспоминаний и ценности жизни в моменте. Это очень нежная и трогательная история о том, как важно двигаться вперед, испытывая новые чувства и эмоции. Главному герою предстоит понять, что значит смириться с горем, балансируя между прошлым и настоящим.', 2024, 18, 6.3, 6.5, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/b/f0/23f1d028e9b551d7bf60744eda1f8bf0-222551-d88fe645138b46868952f57a7359201c.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/b/f0/23f1d028e9b551d7bf60744eda1f8bf0-222551-d88fe645138b46868952f57a7359201c.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/1/51/e0e1aaf929d42e13bf92d60d40feb151-222548-46c4f8c72bfd47239ee72cc0e04fbe47.png', N'Фэнтези', N'США', 'https://www.youtube.com/watch?v=qQVNJOClPPQ')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Игра Престолов. Последний дозор', N'Монументальный документальный фильм, максимально полно раскрывающий особенности создания главного сериала планеты «Игра престолов».', N'Монументальный документальный фильм, максимально полно раскрывающий особенности создания главного сериала планеты «Игра престолов». Режиссер Джини Финлэй получила беспрецедентный доступ к закулисью фэнтези-саги. «Последний дозор» — остроумный, веселый и трогательный рассказ о группе людей, которые своими руками создали невероятный вымышленный мир, захвативший человечество. В этой истории есть все: пот и слезы, грязь и кровь, а также достижение, казалось бы, немыслимых высот. Картина получилась подробным и искренним отчетом о трудностях производства, с которыми столкнулась съемочная группа, выстоявшая в битве с экстремальными погодными условиями, горящими дедлайнами и страстным желанием фанатов выведать спойлеры финального сезона.', 2019, 18, 7.6, 8.5, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/6/32/3be08e15804f47ac4067a2cb7aa4c632-13500-3186948fb8594e54b0e785417bc2a4a1.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/6/32/3be08e15804f47ac4067a2cb7aa4c632-13500-3186948fb8594e54b0e785417bc2a4a1.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/e/f7/743e5990a7d1bd3404f221fb0ee1eef7-13499-7a1fd5e3de014a728c2c069ea9b3d5f5.png', N'Документальный', N'США', 'https://www.youtube.com/watch?v=Ga0WpKNAszM')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Бакстер', N'В духе комедий Говарда Хоукса фильм «Бакстер» рассказывает о перипетиях в жизни молодого человека за две недели до его свадьбы.', N'Американский комедийный фильм Майкла Шоуолтера («Любовь с первого взгляда», «Глаза Тэмми Фэй») при участии Элизабет Бэнкс («Голодные игры»), Мишель Уильямс («7 дней и ночей с Мэрилин») и Джастина Теру («Малхолланд Драйв»). Главный герой картины, заурядный бухгалтер из Бруклина по имени Эллиот (Шоуолтер), живет с уверенностью, что не видать ему счастья в любви и кто-то обязательно отобьет у него невесту Кэролайн (Бэнкс). И вот за неделю до их свадьбы в город неожиданно возвращается Брэдли (Теру), экс-кавалер его девушки. Параллельно с этим временная секретарша Эллиота, красавица Сесиль (Уильямс), заставляет его по-новому взглянуть на свои чувства к невесте. Начинается яркий круговорот событий и нелепых случайностей, которые выведут героев из зоны комфорта и приведут к приятным, но совершенно неожиданным последствиям.', 2005, 18, 6.5, 5.1, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/5/81/1f481e52f97d20a33e0f8f4386f9e581-225505-a7b63eef968746e0bdabf5792de99bc6.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/5/81/1f481e52f97d20a33e0f8f4386f9e581-225505-a7b63eef968746e0bdabf5792de99bc6.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/1/84/101fcc20e1f3f4a8db16216f12ea2184-225501-9b307623714448f2b06029c11edf2ae4.png', N'Комедия', N'США', 'https://www.youtube.com/watch?v=scFfb8qCH3M')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Секрет', N'Дарлин Хэйген, женщина, долго боровшаяся с алкоголизмом после исчезновения дочери, на Рождество сталкивается с бывшим мужем сестры, который приносит мрачный секрет, переворачивающий их жизни.', N'«Секрет» – необычный рождественский триллер с Анной Ганн («Враг государства», «Во все тяжкие») в главной роли. Дарлин Хэйген – женщина, долгие годы страдавшая алкоголизмом, но теперь вставшая на путь трезвости. Ее дочь Салли пропала без вести 20 лет назад, и тоска по ней вогнала женщину в тяжелую депрессию. В свой дом на Рождество она ждет близких людей – друзей и родственников. Но в ночь перед праздником к ней на порог является неожиданный гость – бывший муж ее сестры по имени Джек (Лайнас Роуч, «За гранью», «Викинги»). И принес мужчина с собой не подарки, а тяжелый секрет, который он носил в себе много лет. Между героями начинается настоящая психологическая дуэль, итог которой невозможно предсказать.', 2022, 18, 5.2, 4, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/8/b2/777f55206e8429781b4d0bc7bbc178b2-225235-193f3f8d8b104e0da6855c135a18c890.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/8/b2/777f55206e8429781b4d0bc7bbc178b2-225235-193f3f8d8b104e0da6855c135a18c890.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/e/0a/6a9e3f8bbe00d526c6fd236b7c06de0a-225232-baac3ab35eb1425693326884cec8861f.png', N'Криминал', N'США', 'https://www.youtube.com/watch?v=51lTUZr8hFI')

INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Магия', N'Иллюзионист с куклой-чревовещателем борется с внутренними демонами, выбирая между сценой и тихой жизнью.', N'«Магия» – классический фильм ужасов 1978 года с молодым Энтони Хопкинсом («Молчание ягнят», «Обреченные на славу»), который рассказывает об иллюзионисте Корки, мечтающем стать звездой сцены. К сожалению, все его выступления на публике проваливаются, но как только в его арсенале появляется кукла-чревовещатель Фэтс, любящая сквернословить, всё меняется. Но вместо того, чтобы почивать на лаврах, Корки зачем-то сбегает в глушь, снимает небольшой домик у рыжеволосой красавицы Пегги (Энн-Маргрет, «Десятое королевство») и пытается зажить спокойной жизнью. Фэтсу это жутко не нравится, о чем он не забывает намекать владельцу, да и театральный агент мужчины просит его вернуться на сцену. Что выберет Корки – умиротворяющую тишину или безумную любовь зрителей?', 1978, 18, 6.8, 7.0, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/9/5a/843115b083da5b0f1afbc09c8421195a-235880-a86cc7986daf4f899e4763585103876c.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/9/5a/843115b083da5b0f1afbc09c8421195a-235880-a86cc7986daf4f899e4763585103876c.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/3/95/7dae7ad6cc4955c92d12630756bc2395-235879-4966f47c2fa145cb989247b53b073ec0.png', N'Ужасы', N'США', 'https://www.youtube.com/watch?v=Kabq-mUTaYg')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Раненый олень', N'«Раненый олень» — хоррор с элементами комедии и греческой трагедии. Мередит едет с Брюсом в загородный дом, но сюжет ломает привычные жанровые клише.', N'Необычный фильм ужасов, балансирующий на грани между хоррором и черной комедией, а сверху приправленный еще и древнегреческой трагедией и неоновыми цветами! Главная героиня «Раненого оленя» – простая сотрудница музейного отдела по имени Мередит (Сара Линд, «Фарго», «Секс в большом городе»). Она переживает личный кризис из-за того, что ее последние отношения были крайне неудачными. Однажды девушка знакомится с симпатичным мужчиной Брюсом (Джош Рубен, «Адам портит всё», «Жуткая правда»), который увозит ее на выходные в свой уютный загородный дом. И если вы думаете, что дальше начнется уже ставшая классической история про маньяка-убийцу и его жертву, то авторы фильма приготовили для вас ряд шокирующих поворотов сюжета, меняющих «правила игры»!', 2022, 18, 5.5, 2.2, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/c/1c/7fac9763421952616688e6b82500ec1c-227725-2ce6121ba298498d9dfce0ff25a0de1b.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/c/1c/7fac9763421952616688e6b82500ec1c-227725-2ce6121ba298498d9dfce0ff25a0de1b.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/2/2b/e4931efa98353abc02f3525a0771a22b-227723-670414005141432dbffb55f266dd85d1.png', N'Ужасы', N'США', N'https://www.youtube.com/watch?v=RePmDvUsxTk')
INSERT INTO movies (title, description_card, description, release_year, rating, imdb_rating, amediateka_rating, poster_url, bg_url, card_url, genre, country, url_film) VALUES (N'Тони Хоук: Пока не отвалятся колеса', N'Документальный фильм о самом влиятельном скейтбордисте всех времен. «Тони Хоук: Пока не отвалятся колеса» — всесторонний и исчерпывающий взгляд на жизнь и карьеру легендарного спортсмена. На протяжении долгих лет Тони Хоук был главной поп-звездой скейтбординга, ролевой моделью для многих людей, пытающихся покорить «доску». Создатели проекта получили беспрецедентный доступ к скейтбордисту и редкие архивные кадры. Фильм также включает в себя интервью с видными коллегами Хоука — Родни Малленом, Майком МакГиллом, Энди Макдональдом и многими другими.', N'Документальный фильм о самом влиятельном скейтбордисте всех времен. «Тони Хоук: Пока не отвалятся колеса» — всесторонний и исчерпывающий взгляд на жизнь и карьеру легендарного спортсмена. На протяжении долгих лет Тони Хоук был главной поп-звездой скейтбординга, ролевой моделью для многих людей, пытающихся покорить «доску». Создатели проекта получили беспрецедентный доступ к скейтбордисту и редкие архивные кадры. Фильм также включает в себя интервью с видными коллегами Хоука — Родни Малленом, Майком МакГиллом, Энди Макдональдом и многими другими.', 2022, 18, 8.0, 9.0, N'https://i.amediateka.tech/resize/960x480/_stor_/cms/content-contentasset/4/06/e5881a8c75b542448655c1b853318406-133602-e554fa5afc43496e89eb3ba3e7e9d62e.jpg', N'https://i.amediateka.tech/resize/1920x960/_stor_/cms/content-contentasset/4/06/e5881a8c75b542448655c1b853318406-133602-e554fa5afc43496e89eb3ba3e7e9d62e.jpg', N'https://i.amediateka.tech/trim/640x320/_stor_/cms/content-contentasset/9/91/f4c8721b2d6cf7ff0743229d35fbd991-133601-cf2962e1cf924a979844b494d819dd1a.png', N'Документальный', N'США', 'https://www.youtube.com/watch?v=iqhmW_etP38')

INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (1, 1, 1)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (2, 2, 2)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (3, 3, 3)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (4, 4, 4)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (5, 5, 5)

INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (6, 6, 6)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (7, 7, 7)
INSERT INTO movie_staff (movie_id, actor_id, director_id) VALUES (8, 8, 8)

INSERT INTO movie_genres (movie_id, genre_id) VALUES (1, 14)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (2, 13)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (3, 1)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (4, 16)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (5, 20)

INSERT INTO movie_genres (movie_id, genre_id) VALUES (6, 21)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (7, 21)
INSERT INTO movie_genres (movie_id, genre_id) VALUES (8, 1)


INSERT INTO movie_countries (movie_id, country_id) VALUES (1, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (2, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (3, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (4, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (5, 1)

INSERT INTO movie_countries (movie_id, country_id) VALUES (6, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (7, 1)
INSERT INTO movie_countries (movie_id, country_id) VALUES (8, 1)

INSERT INTO genres ( name) VALUES (N'Документальный')
INSERT INTO genres ( name) VALUES (N'Мелодрама')
INSERT INTO genres ( name) VALUES (N'Фантастика')
INSERT INTO genres ( name) VALUES (N'Стендап')
INSERT INTO genres ( name) VALUES (N'Анимация')
INSERT INTO genres ( name) VALUES (N'Драма')
INSERT INTO genres ( name) VALUES (N'Музыка')
INSERT INTO genres ( name) VALUES (N'Фильмы HBO')
INSERT INTO genres ( name) VALUES (N'Вестерн')
INSERT INTO genres ( name) VALUES (N'Биография')
INSERT INTO genres ( name) VALUES (N'Исторический')
INSERT INTO genres ( name) VALUES (N'Спорт')
INSERT INTO genres ( name) VALUES (N'Фэнтези')
INSERT INTO genres ( name) VALUES (N'Короткий метр')
INSERT INTO genres ( name) VALUES (N'Военный')
INSERT INTO genres ( name) VALUES (N'Комедия')
INSERT INTO genres ( name) VALUES (N'Триллер')
INSERT INTO genres ( name) VALUES (N'Шоу')
INSERT INTO genres ( name) VALUES (N'Детектив')
INSERT INTO genres ( name) VALUES (N'Криминал')
INSERT INTO genres ( name) VALUES (N'Ужас')
INSERT INTO genres ( name) VALUES (N'Семейный')

INSERT INTO directors ( name) VALUES (N'Неизвестно')
INSERT INTO directors ( name) VALUES (N'Эмили Тин')
INSERT INTO directors ( name) VALUES (N'Неизвестно')
INSERT INTO directors ( name) VALUES (N'Майкл Шоуолтер')
INSERT INTO directors ( name) VALUES (N'Элисон Лок')

INSERT INTO directors ( name) VALUES (N'Ричард Аттенборо')
INSERT INTO directors ( name) VALUES (N'Трэвис Стивенс')
INSERT INTO directors ( name) VALUES (N'Неизвестно')


INSERT INTO countries ( name) VALUES (N'США')
INSERT INTO countries ( name) VALUES (N'Россия')
INSERT INTO countries ( name) VALUES (N'Великобритания')
INSERT INTO countries ( name) VALUES (N'Германия')
INSERT INTO countries ( name) VALUES (N'Нидерланды')
INSERT INTO countries ( name) VALUES (N'Япония')
INSERT INTO countries ( name) VALUES (N'Пакистан')
INSERT INTO countries ( name) VALUES (N'Австралия')
INSERT INTO countries ( name) VALUES (N'Канада')
INSERT INTO countries ( name) VALUES (N'Мексика')

INSERT INTO actors ( name) VALUES (N'Неизвестно')
INSERT INTO actors ( name) VALUES (N'Пейтон Лист, Майкл Чимино, Фиби Холден')
INSERT INTO actors ( name) VALUES (N'Неизвестно')
INSERT INTO actors ( name) VALUES (N'Майкл Шоуолтер, Элизабет Бэнкс, Мишель Уильямс, Джастин Теру')
INSERT INTO actors ( name) VALUES (N'Анна Ганн, Лайнас Роуч, Джанин Гарофало, Зена Ли')

INSERT INTO actors ( name) VALUES (N'Энтони Хопкинс, Энн-Маргрет, Бёрджесс Мередит')
INSERT INTO actors ( name) VALUES (N'Сара Линд, Джош Рубен, Кэти Куан')
INSERT INTO actors ( name) VALUES (N'Неизвестно')


INSERT INTO users(email, password, is_admin) VALUES ('ilham_sh2005@mail.ru', 'asd2005-', 0);
INSERT INTO users(email, password, is_admin) VALUES ('admin@mail.ru', 'admin', 1);




