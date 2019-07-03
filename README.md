﻿# Цифровой Светофор

Цифровой светофор ­ светофор, оснащенный индикатором цвета и циферблатом. [Задание](https://github.com/BolatovAlau/Trafic/blob/master/%D0%A2%D0%B5%D1%81%D1%82%D0%BE%D0%B2%D0%BE%D0%B5%20%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5%20%D0%A6%D0%B8%D1%84%D1%80%D0%BE%D0%B2%D0%BE%D0%B9%20%D0%A1%D0%B2%D0%B5%D1%82%D0%BE%D1%84%D0%BE%D1%80%20(b).pdf)

## Установка

Можете клонировать репозиторий или загрузить файл. 
Для того чтобы установить библиотеки
> Сборка -> Пере собрать решения

Чтобы запустить тесты
> Тест -> Отладка -> Все тесты (Ctrl+L, Ctrl+A)

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

## Библиотеки

| Plugin | LINK |
| ------ | ------ |
| Sqlite | https://www.sqlite.org/ |
| Json.NET | https://www.newtonsoft.com/json |
| NUnit | https://nunit.org/ |
| Moq | https://github.com/moq/moq4 |

##  Оптимизация

1. Как можно раньше найти, с какого числа был начал отсчёт.
> Допустим есть несколько вариантов, Каждое следующее число должно быть в шаблоне минус ряд порядка регресса. Например, отсчет начался с 87, значит следующее число должно быть в шаблоне 86, потом 85 и т.д. Сортировать всех чисел, До тех пор, пока будет только один возможный вариант.
2. Как можно точнее определить, какие секции точно сломаны.
> Нужно рядом с каждым возможным числом сохранять список сломанных для этого числа датчиков. 
Например для числа 98 (0101000,1000100), 68 (0100001,0001000) ... При уменьшении возможных вариантов мы узнаем что им не хватало этих датчиков для отображения чисел.
3. Отсутствие деградации производительности при большом количестве последовательностей и наблюдений.
> От 99 до 1 за ≈60мс, с восемью сломанными датчиками, (NUnit)

## Лицензия
[MIT](https://choosealicense.com/licenses/mit/)
