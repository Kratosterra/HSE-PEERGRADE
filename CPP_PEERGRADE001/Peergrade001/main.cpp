// Подключаем заголовочный файл и файл с исполняемым кодом.
#include "Functions.h"
#include "Functions.cpp"


/**
 *  @brief  Основная функция работы программы.
 *  @return  0 при успешном выполнении, -1 при возникновении ошибки.
*/

int main() {
    // Устанавливаем кодовую страницу для вывода русского языка.
    SetConsoleOutputCP(CP_UTF8);
    // Переменная для проверки опции выхода из программы.
    const int32_t EXIT_NUM = 10;
    // Текущий выбор меню пользователем.
    int32_t now_choice;
    // Задаем граф.
    Graph graph = {false};
    // Устанавливаем цвет консоли.
    system("color 0B");
    // Показываем заставку.
    ShowStartOfProgram();
    cout << "── Запрос ────────────────────────────────────────\n";
    cout << "Нажмите Enter, чтобы продолжить...";
    cin.ignore(std::numeric_limits<streamsize>::max(),'\n');
    do {
        system("color 0B");
        // Показываем текущий граф.
        ShowNowGraphInfo(graph);
        // Показываем стартовое меню.
        ShowMainMenu();
        // Получаем выбор пользователя.
        now_choice = GetChoice();
        // Если выбор является приемлемым - работаем.
        DoGraphWork(now_choice, graph);
        Sleep(200);
        cout << "── Запрос ────────────────────────────────────────\n";
        cout << "Нажмите Enter, чтобы продолжить...";
        cin.ignore(std::numeric_limits<streamsize>::max(),'\n');
    } while (now_choice != EXIT_NUM);
    // Показываем конечную заставку.
    ShowExitOfProgram();
    return 0;
}
