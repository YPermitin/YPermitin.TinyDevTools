# YPermitin.TinyDevTools

Blazor-приложение с небольшим набором инструментов для разработчиков.

## Обратная связь и новости

Вопросы, предложения и любую другую информацию [отправляйте на электронную почту](mailto:i.need.ypermitin@yandex.ru).

Новости по проектам или новым материалам в [Telegram-канале](https://t.me/TinyDevVault).

## Окружение для разработки

Для окружение разработчика необходимы:

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/ru/vs/)

## Состав проекта

Проекты и библиотеки в составе решения:

* YPermitin.TinyDevTools.Client - клиентское веб-приложение с инструментами разработчика на основе Blazor WebAssembly.
* YPermitin.TinyDevTools.API - серверное API с инструментами разработчика. Может использоваться как отдельно сторонними приложениями, так и используется веб-приложением TinyDevTools.

## Инструменты

Список инструментов и их описание.

| Инструмент | Описание |
| ---------- | -------- |
| Генератор GUID | Формирование списка GUID по настройкам (количество, отображение дефисом, регистр символов, фигурные скобки, BASE64 и др.) |
| Мой IP | Просмотр информации о клиенте в части IP-адреса, браузера и некоторые другие свойства клиентского запроса. |
| ФИАС | Инструменты работы с ФИАС (информация о дистрибутиве, импорт, поиск адресов и другое). |
