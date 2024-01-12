# Projekt Library

## Lista wykorzystanych technologii:
- ASP.NET w wersji 8
- Baza danych SQL Server LocalDB
- Entity Framework
- Zmodyfikowana wersja biblioteki Bootstrap 5 z motywem Hope UI
- Xunit do obsługi testów jednostkowych

---

## Dane przykładowych użytkowników:

### Rola: StandardUser
- Login: user@example.com
- Hasło: User@123

### Rola: Admin
- Login: admin@example.com
- Hasło: Admin@123

Powyższe konta są gotowe do użycia od razu po uruchomieniu aplikacji.

---

## Proces uruchomienia aplikacji w wersji deweloperskiej:

1. Uruchomienie Solution o nazwie `Library-university-aspnet.sln` oraz pobranie wymaganych dependencies.
2. Utworzenie nowej migracji (`add-migration Initial`) w Package Manager Console a następnie wpisanie komendy `update-database`.
3. Zalogować się danymi administratora, aby przetestować działania funkcji CRUD.

---

## Własne funkcje aplikacji:

- Własna modyfikacja wyglądu aplikacji.
- Jeśli podczas edycji książki zmienią się dane autora/gatunku i obiekt o podanej nazwie nie znajduje się obecnie w bazie danych, to zostanie on utworzony.
- Testy jednostkowe w Xunit dla `BookService`, `AuthorService` i `GenreService` aby przetestować ich prawidłowe działanie.
- `ReservationService` do obsługi rezerwacji (natomiast nie jest on obecnie nigdzie użyty).
