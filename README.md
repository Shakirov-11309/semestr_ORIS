# Предупреждение
На сайте отсутсвует рабочий фильтр и не совсем рабочая админ панель
# Шаг 1:
Скачиваем проект или клонируем
# Шаг 2: 
## Настройка проекта
Для того чтобы запустить проект необходимо открыть MyHtttpServer.sln. В открывшимся редакаторе (желательно Visual Studio) необходимо подключить базу данных, чтобы её подключить к нашему проекту надо открыть "Образователь объектов SQL Server". После чего нажимаем на кнопку добавить SQL Server. 
Далее в полях: 
- имя сервера пишем localhost
- Проверка подлинности выбираем: Проверка подлинности SQL Server
- Имя пользователя: sa
- Пароль: P@ssw0rd
- Доверять сертификату сервера: true
- Остальное не трогаем

В нашем созданом SQL Server создаем базу данных, а затем открываем файл TablesFilm.sql копируем все содержимое и вставляем его в запрос.
# Шаг 3:
Запускаем проект

/movies - получаем каталог фильмов

/login - авторизация

Чтобы получить доступ к админ панеле необходимо нажать на свой профиль. Если не открывается админ панель, значит у вас нет прав доступа к нему 
