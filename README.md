# Xamarin Starting


- Project for iOS and Andorid
- With Shared code assembly
- Загружает новости, используя REST API NY Times: https://developer.nytimes.com/
- REST Client library [paulcbetts/refit: The automatic type-safe REST library for Xamarin and .NET](https://github.com/paulcbetts/refit)
- Отображение списка новостей средствами Xamarin.Forms ListView (CachingStrategy = RecycleElement)


## задание изменить текущее тестовое приложение на Forms

- больше данных в списке, можно текущие соединить пять шесть раз, не меньше 200 строк
- кастомный темплейт ячейки
- ImageCache а не постоянный запрос изображений при прокрутке
- отмена предыдущего запроса при recycle ячейки
- страницу с деталями, не только список, при щелчке на строску переходим на страницу с деталями

## Решение

- больше данных в списке, можно текущие соединить пять шесть раз, не меньше 200 строк
 
OK


- кастомный темплейт ячейки

Структура ячейки ArticleRowViewCell создается в коде


- ImageCache а не постоянный запрос изображений при прокрутке

new UriImageSource{ CachingEnabled = true, Uri = imageUrl };


- отмена предыдущего запроса при recycle ячейки

через UriImageSource.Cancel();


- страницу с деталями, не только список, при щелчке на строску переходим на страницу с деталями

OK + Открывается браузер 

## TODO

- TODO: проверить отмену загрузки картинки ячейки
- TODO: Использовать стили: https://developer.xamarin.com/guides/xamarin-forms/user-interface/styles/
- TODO: Вынести статические значения в ресурсы 
- TODO: Попробовать FFImageLoading для загрузки картинок
- TODO: Проработать обработку ошибок и отображение сообщений пользователю 
- TODO: Выделить логику фабрики комнонентов из роутера

- работал на маке в XamarinStudio (без решарпера)
- стартовый экран с кнопкой перехода к новостям оставил, чтобы можно было пересоздавать список новостей
