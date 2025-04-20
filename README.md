# Zoo Management

Проект **Zoo Management** реализует систему управления зоопарком с применением концепций Domain‑Driven Design (DDD) и принципов Clean Architecture. Ниже представлен обзор функционала, структуры проекта и применённых архитектурных решений.

## Содержание

- [Реализованный функционал](#реализованный-функционал)
- [Структура проекта](#структура-проекта)
- [Domain‑Driven Design](#domain-driven-design)
- [Clean Architecture](#clean-architecture)
- [Тестирование](#тестирование)

## Реализованный функционал

| №  | Функционал                                | Модуль / Класс                                           |
|----|-------------------------------------------|----------------------------------------------------------|
| 1  | Добавление / удаление животного           | `AnimalsController` (Presentation)                       |
|    |                                           | `InMemoryAnimalRepository` (Infrastructure)              |
| 2  | Добавление / удаление вольера             | `EnclosuresController` (Presentation)                    |
|    |                                           | `InMemoryEnclosureRepository` (Infrastructure)           |
| 3  | Перемещение животного в другой вольер     | `POST /api/animals/{id}/transfer/{enclosureId}`         |
|    |                                           | `AnimalTransferService` (Application)                    |
| 4  | Просмотр расписания кормления             | `FeedingController` (Presentation)                       |
|    |                                           | `InMemoryFeedingScheduleRepository` (Infrastructure)     |
| 5  | Добавление нового кормления               | `FeedingOrganizationService` (Application)               |
| 6  | Отметка выполнения кормления              | `FeedingController` / `FeedNowAsync`                     |
| 7  | Просмотр статистики зоопарка              | `StatisticsController` (Presentation)                    |
|    |                                           | `ZooStatisticsService` (Application)                     |

## Структура проекта

```
ZooManagement.sln
├── ZooManagement.Domain           # Доменные сущности и события
├── ZooManagement.Application      # Бизнес‑логика (сервисы, интерфейсы)
├── ZooManagement.Infrastructure   # Реализации репозиториев (in-memory)
├── ZooManagement.Presentation     # ASP.NET Core Web API (контроллеры, Swagger)
└── ZooManagement.Tests            # xUnit‑тесты (покрытие >65%)
```

## Domain‑Driven Design

- **Сущности (Entities)**: `Animal`, `Enclosure`, `FeedingSchedule` инкапсулируют идентичность и логику.
- **Доменные события**: `AnimalMovedEvent`, `FeedingTimeEvent` генерируются внутри сущностей.
- **Value Objects**: перечисления `Gender`, `FoodType`, `HealthStatus`, `EnclosureType`.
- **Инкапсуляция бизнес‑правил**: проверки вместимости, статуса здоровья и т.д. реализованы внутри объектов.

## Clean Architecture

- **Разделение на слои**:
  - **Domain**: не зависит ни от чего.
  - **Application**: зависит от Domain.Interfaces.
  - **Infrastructure**: зависит от Domain.Interfaces.
  - **Presentation**: зависит от Application.Interfaces и Domain.Interfaces.
- **Взаимодействие через интерфейсы**:
  - Репозитории (`IAnimalRepository` и др.) и сервисы (`IAnimalTransferService` и др.).
- **Изоляция логики**:
  - Контроллеры делегируют обработку в Application и Domain, не содержат бизнес‑правил.

## Тестирование

- **Юнит‑тесты** на xUnit для Domain, Application и Infrastructure.
- **Покрытие** кода 80% благодаря тестированию всех ключевых методов и сервисов.

