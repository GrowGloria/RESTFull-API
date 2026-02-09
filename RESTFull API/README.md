Понелась.

dotnet tool install --global dotnet-ef

Для миграций.
dotnet ef migrations add Init
dotnet ef database update

🛠 Установка и запуск

1️⃣ Клонирование репозитория

	git clone <repo-url>
	cd RESTFull_API

2️⃣ Настройка подключения к БД

В файле appsettings.json:

	"ConnectionStrings": { "Default": "Host=localhost;Port=5432;Database=CRM_Storage;Username=postgres;Password=YOUR_PASSWORD" }


⚠ Убедитесь, что PostgreSQL запущен и база существует.

3️⃣ Применение миграций
dotnet ef database update

4️⃣ Запуск проекта
	
	dotnet run


Swagger будет доступен по адресу:

	https://localhost:<port>/swagger

🧪 Тестирование

Тесты написаны с использованием xUnit и Moq.

Запуск тестов:

	dotnet test


Тесты не зависят от реальной базы данных.
Репозиторий мокируется через интерфейс IRollRepository.