# VMControlService
Project "Hosting" for managing virtual machines on a microservice architecture with user accounts



MainApplication отвечает за фронт и общее взаимодейтсвие приложения, hyper-v это сервис для взаимодействия с ВМ

В апи находятся контроллеры и endpoint'ы, restful методы post, get

Взаимодействие фронта и бека происходит по rest запросам

-MainApplication.API

Controllers это наши апи которые содержат в себе rest методы

Так же в MainApplication.API есть папка wwwroot в которой находятся статические страницы html и js

В папке ViewModels находятся view и request модели, они используются в контроллерах для принятия и отображения данных

Program.cs это системный файл который отвечает за билд сборки

Startup.cs это конфигурационный файл для регистрации сервисов, настройки аут и реги

appsettings.json так же конфиг файл, но его можно изменить без перестройки решения

launchsettings.json файл среды приложения, порты и ssl сертификаты настраиваем в нём

-MainApplication.BL

В папке Entities находятся модели которые хранятся в базе

Interfaces содержит интерфейсы сервисов папки Services

Migrations папка миграций, простым языком- определяет схему бд и связи

ApplicationContext отвечает за подключение к БД, другими словами провайдер БД

AuthOptions содержит в себе конфиги для токенов


-HyperV.APi

HomeController основное апи взамодействия 

VmActionRequest модель взаимодействия используемая в HomeController для выдачи и принятия данных

appsettings.json..

program.cs..

startup.cs...

-HyperV.BL

ref и runtimes папки sdk сборка для PowerShell

app.manifest так же конфиг файл приложения

AuthOptions.cs конфиг класс для токена

IVmService интерфейс сервиса VmService
