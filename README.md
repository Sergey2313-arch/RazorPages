# Contoso University — ASP.NET Core Razor Pages

Учебное веб-приложение для управления данными университета, созданное на **ASP.NET Core Razor Pages и .NET 8**.

Проект демонстрирует работу с реляционной моделью данных, Entity Framework Core, SQL Server, Razor Pages и CRUD-операциями.

## Возможности

- управление студентами;
- управление курсами;
- запись студентов на курсы;
- хранение оценок;
- работа с преподавателями и кафедрами;
- начальное заполнение базы тестовыми данными;
- CRUD-страницы;
- асинхронная работа с Entity Framework Core;
- миграции базы данных.

## Модель данных

Основные сущности:

- `Student`;
- `Course`;
- `Enrollment`;
- `Instructor`;
- `Department`;
- `OfficeAssignment`.

Проект содержит связи one-to-many, many-to-many и связанные навигационные свойства.

## Технологии

- C#;
- .NET 8;
- ASP.NET Core Razor Pages;
- Entity Framework Core;
- SQL Server;
- Razor;
- Bootstrap;
- LINQ.

## Локальный запуск

Требования:

- .NET 8 SDK;
- SQL Server или SQL Server LocalDB;
- установленный инструмент `dotnet-ef`.

```bash
git clone https://github.com/Sergey2313-arch/RazorPages.git
cd RazorPages
dotnet restore
dotnet ef database update
dotnet run
```

Перед запуском укажите корректную строку подключения к SQL Server в конфигурации приложения.

## Что демонстрирует проект

- проектирование связанных сущностей;
- настройку `DbContext`;
- загрузку и отображение связанных данных;
- CRUD через Razor Pages;
- создание и применение миграций;
- заполнение базы начальными данными;
- работу с LINQ и асинхронными запросами.

## Статус

Учебный проект, подготовленный для портфолио начинающего .NET-разработчика.

## Автор

**Sergey Korobkov**  
Junior .NET / ASP.NET Core Developer
