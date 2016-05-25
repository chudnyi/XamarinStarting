# Xamarin Starting

My first project on Xamarin

- Project for iOS and Andorid
- With Shared code assembly
- Загружает новости, используя REST API NY Times: https://developer.nytimes.com/
- REST Client library [paulcbetts/refit: The automatic type-safe REST library for Xamarin and .NET](https://github.com/paulcbetts/refit)
- Отображение списка новостей средствами Xamarin.Forms ListView (CachingStrategy = RecycleElement)



## ListView 

~~~~
<mr:ListView ...>
        <x:Arguments>
            <ListViewCachingStrategy>RecycleElement</ListViewCachingStrategy>
        </x:Arguments>
~~~~
