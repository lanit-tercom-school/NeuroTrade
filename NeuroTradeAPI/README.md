# ASP.NET API + PostgreSQL: Developer's environment
Пока тут только Rider проект, созданный по шаблону ASP.NET Core Web API. Он соединяется с некоторой БД в PostgreSQL на компьютере и автоматически генерирует там таблицы.
**Исправьте ошибки, пожалуйста, если увидите**

# Как запустить
* установить .Net Core, PostgreSQL;
* сделать pull репозитория;
* создать у себя пользователя СУБД и БД (см. далее);
    * названия и пароль менять не стоит, потому что они используются в коде.

## Про Linux
``` bash
$ sudo su postgres
$ createdb nt_db
$ psql -s nt_db
# create user nt_dev password 'weakpassword';
# GRANT ALL PRIVILEGES ON DATABASE nt_db TO nt_dev;
```

## Про Windows
* После установки Postgres сервер должен запуститься и слушать порт 5432;
* Заглянуть в базу можно с помощью программки pgAdmin или из самого Rider (View->Tool windows->Database);
	* если нет соединения, значит надо запустить postgres: находим его в сервисах и включаем (в консоли ```services.msc```);
	* если тоже забыли пароли через 5 минут после придумывания [см тут];
* Дальше пара SQL запросов из программы ниже (от имени пользователя postgres):

```sql
CREATE USER nt_dev PASSWORD 'weakpassword' CREATEDB INHERIT LOGIN;
CREATE DATABASE nt_db OWNER nt_dev;
```

# Finally
* сделать rebase, чтобы .Net подгрузил себе всё необходимое
    * в этой папке (возможно, нужны права sudo, а может это и не нужно вовсе): `dotnet rebase`
    * чтобы проверить, что всё окей: `dotnet build`
    * если в проекте изменилась модель, а в локальной базе данных - нет, то понадобится `dotnet ef database update`

# Прочее
## Доступные API вызовы
* Список инструментов и наборов[/api/v0/quotes](http://localhost:5000/api/v0/quotes);
* загрузка данных с биржи [/addjob.html](http://localhost:5000/addjob.html);
* поиск по инструментам с фильтрами [/api/v0/quotes](http://localhost:5000/api/v0/quotes/filter) с параметрами:
	* [instrument=...](http://localhost:5000/api/v0/quotes/filter?instrument=1) - поиск по id или названию тикера,
	* [batch=...](http://localhost:5000/api/v0/quotes/filter?batch=1) - поиск по id набора,
	* [from и to](localhost:5000/api/v0/quotes/filter?instrument=1&from=2017-12-05T13:00:00&to=2017-12-05T15:00:00) - для временного среза, оба опциональные. Формат даты 2017-12-05 13:00:00 (разделитель - пробел или буква T; можно не указывать время) гарантированно работает.

## Возможные непонятки
* Проект `Repository` есть, но он пока возможно не будет работать, потому что БД сейчас меняется активно, и ради простоты добавлена конфигурация сборки `Without Repo`.
* По той же причине могут возникать ошибки во время миграции базы данных (`dotnet ef database update`). В этом случае можно руками удалить таблицы из БД и запустить SQL скрипт, который лежит в папке "Database related misc". В нём ещё чуть больше правил, чем позволяет сделать Entity Framework.

[см тут]:https://dba.stackexchange.com/questions/44586/forgotten-postgresql-windows-password
