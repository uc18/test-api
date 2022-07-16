Тестовый проект, от 2021 года, в одну из российских компаний по продаже цифровых ключей

Суть задания заключалась в реализации тестового API, в котором можно добавлять и удалять пользователей
Клиентом тестирования являлся Swagger

Стек:

* .NET 5
* C# 9.0  
* SQLite  

Перед запуском, обязательно прогнать применение миграций. Они создают данные по группам и существующим статусам, для работы.


# Создание миграций
dotnet ef migrations add *Название_миграций* -p .\src\MySolution.DAL\MySolution.DAL.csproj -s .\src\MySolution.Web\MySolution.Web.csproj

# Применение миграций
dotnet ef database update -p .\src\MySolution.DAL\MySolution.DAL.csproj -s .\src\MySolution.Web\MySolution.Web.csproj