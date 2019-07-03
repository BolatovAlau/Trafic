﻿# Тестовое задание, Цифровой Светофор 

Цифровой светофор ­ светофор, оснащенный индикатором цвета и циферблатом. 

## Установка

Можете склонировать репозиторий или загрузить файл. После, для того чтобы установить библиотеки
> Сборка -> Пересобрать решения

Чтобы запустить тесты
> Тест -> Отладка -> Все тесты (Ctrl+l, Ctrl+A)

## Запросы

```c#
http://localhost:xxxx/sequence/create // HttpPost, Создает новую последовательность
Запрос: без тела
Ответ: {'status':'ok','response':{'sequence':'b839e67c­d637­4afc­9241­63943c4fea83'}}

http://localhost:xxxx/observation/add // HttpPost, Добавляет наблюдение для последовательности
Запрос: {'observation':{'color':'green','numbers':['1110111','0011101']},'sequence':'b839e67c­d637­4afc­9241­63943c4fea83'}
Ответ: {'status':'ok','response':{'start':[2,8,82,88],'missing':['0000000','1000000']}}

http://localhost:xxxx/clear // HttpGet, Очистка базы
Запрос: без тела
Ответ: {'status':'ok','response':'ok'}
```

## Библиотки

| Plugin | LINK |
| ------ | ------ |
| Sqlite | https://www.sqlite.org/ |
| Json.NET | https://www.newtonsoft.com/json |
| NUnit | https://nunit.org/ |

## Как это работает

[Задание](https://github.com/BolatovAlau/Trafic/blob/master/TraficLight/%D0%A2%D0%B5%D1%81%D1%82%D0%BE%D0%B2%D0%BE%D0%B5%20%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5%20%D0%A6%D0%B8%D1%84%D1%80%D0%BE%D0%B2%D0%BE%D0%B9%20%D0%A1%D0%B2%D0%B5%D1%82%D0%BE%D1%84%D0%BE%D1%80%20(b).pdf)

Назавем горящих секциях на циферблате просто палками для упращения.

Сохраняем горящих 

## Лицензия
[MIT](https://choosealicense.com/licenses/mit/)
