Plik wykonywalny aplikacji znajduje się w katalogu LoadTester/bin/release.

Obsługa:
1. W polu "Target host" należy podać pełny adres testowanej strony (łącznie z prefixem http lub https).
2. W polu "Number of requests" podajemy liczbę zapytań, jakie ma wykonać program. Domyślna wartość to 1.
3. W polu "Ramp-up period" podajemy czas w sekundach, na przestrzeni którego zostaną wykonane zapytania. Domyślna wartość to 1 s.
4. Po podaniu danych i kliknięciu "Send", program wyśle zadaną liczbę zapytań do wskazanego serwera i zmierzy czas odpowiedzi na nie.
Jeśli test przebiegnie pomyślnie, po jego zakończeniu wyświetli się wykres z wynikami pomiarów, a w oknie głównym wypisane zostaną średni czas odpowiedzi, wartość minimalna i maksymalna.