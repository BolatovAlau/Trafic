﻿# Тестовое задание, Цифровой Светофор 

Цифровой светофор ­ светофор, оснащенный индикатором цвета и циферблатом. 

## Установка

Можете склонировать репозиторий или загрузить файл
После, для того чтобы установить библиотеки
> Сборка -> Пересобрать решения


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

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Лицензия
[MIT](https://choosealicense.com/licenses/mit/)
