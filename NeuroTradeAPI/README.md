# ASP.NET API + PostgreSQL: Developer's environment
Пока тут только Rider проект, созданный по шаблону ASP.NET Core Web API. Он соединяется с некоторой БД в PostgreSQL на компьютере и автоматически генерирует там таблицы.
**Исправьте ошибки, пожалуйста, если увидите**

## Как запустить
* установить .Net Core, PostgreSQL;
* сделать pull репозитория;
* создать у себя пользователя СУБД и БД;
    * названия и пароль менять не стоит, потому что они используются в коде;
``` bash
$ sudo su postgres
$ createdb nt_db
$ psql -s nt_db
# create user nt_dev password 'weakpassword';
# GRANT ALL PRIVILEGES ON DATABASE nt_db TO nt_dev;
```
* сделать rebase, чтобы .Net подгрузил себе всё необходимое
    * в этой папке (возможно, нужны права sudo): `dotnet rebase`
    * чтобы проверить, что всё окей: `dotnet build`
