#include <iostream>
#include <utility>
#include <vector>
#include <set>
#include <limits>
#include <Windows.h>
#include <cstdio>
#include <fstream>
#include <cmath>
#include <stack>
#include <queue>
#include "Functions.h" // Указываем заголовочный файл;
// TODO: Отсортировать библотеки по алфавиту.


// Укажем использование пространства имен std.
using namespace std;


struct Graph {
    bool Exist = false;  // Существует ли граф.
    bool Oriented = false;  // Ориентированный ли граф.
    bool WithLoops = false; // C петлями ли он.
    bool Pseudo = false;  // Псевдограф или нет.
    bool Multi = false; // Мультиграф или нет.
    std::string type{};  // Ориентированный ли граф.
    int32_t p = 0;  // Количество вершин.
    int32_t q = 0;  // Количество рёбер.
    int32_t arcs = 0; // Количество дуг.
    int32_t loops = 0; // Количество петель.
    // Cтепени или полустепени вершин.
    std::vector<std::vector<int>> vertex_degrees{};
    // Основные 4 представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<std::vector<int>> incidence_matrix{};
    std::vector<std::vector<int>> adjacency_list{};
    std::vector<std::vector<int>> ribs_list{};
    // FO представление.
    std::vector<int> fo{};
    // FI представление.
    std::vector<int> fi{};
    // MFO представление.
    std::vector<int> mfo_me{};
    std::vector<int> mfo_mv{};
    // MFI представление.
    std::vector<int> mfi_me{};
    std::vector<int> mfi_mv{};
    // BMFO представление.
    std::vector<int> bmfo_me{};
    std::vector<int> bmfo_mv{};
    // BFO представление.
    std::vector<int> bfo_fo{};
};


/**
 *  @brief  Основная функция работы программы.
 *  @return  0 при успешном выполнении, -1 при возникновении ошибки.
*/

int main() {
    SetConsoleOutputCP(CP_UTF8);  // Устанавливаем кодовую страницу для вывода русского языка.
    const int32_t EXIT_NUM = 9;  // Переменная для проверки опции выхода из программы.
    int32_t now_choice;  // Текущий выбор меню пользователем.
    Graph graph = {false}; // Задаем граф.
    system("color 0B");  // Устанавливаем цвет консоли.
    ShowStartOfProgram();  // Показваем заставку.
    cout << "── Запрос ────────────────────────────────────────\n";
    cout << "Нажмите Enter, чтобы продолжить...";
    cin.ignore(std::numeric_limits<streamsize>::max(),'\n');
    do {
        system("color 0B");
        ShowNowGraphInfo(graph);  // Показываем текущий граф.
        ShowMainMenu(); // Показываем стратовое меню.
        now_choice = GetChoice(); // Получаем выбор пользователя.
        // Если выбор является приемлемым - рабоатем.
        DoGraphWork(now_choice, graph);
        Sleep(200);
        cout << "── Запрос ────────────────────────────────────────\n";
        cout << "Нажмите Enter, чтобы продолжить...";
        cin.ignore(std::numeric_limits<streamsize>::max(),'\n');
    } while (now_choice != EXIT_NUM); // Пока человек не пожелал выйти продолжаем работу.
    ShowExitOfProgram(); // Показываем конечную заставку.

    return 0;
}

void ShowStartOfProgram() {
    std::cout << "Это заставка\n";
}

void ShowExitOfProgram() {
    std::cout << "Спасибо за использование!\n";
    system("color 0B");
}

void ShowMainMenu() {
    std::cout << "[1] Ввод графа\n"
                 "[2] Вывод графа\n"
                 "[3] Смена типа представления/хранения\n"
                 "[4] Подсчет степеней/полустепеней вершин\n"
                 "[5] Подсчет ребер/дуг\n"
                 "[6] Обход графа в глубину (стандартно)\n"
                 "[7] Обход графа в глубину (рекурсивно)\n"
                 "[8] Обход графа в ширину\n"
                 "[9] Выйти из программы\n"
                 "─────────────────────────────────────────────\n";
}

void ShowNowGraphInfo(const Graph& graph) {
    std::cout << "\n───────────────────────────────────────────────"
                 "───────────────────────────────────────────\n"
                 "Главное меню\n────────────────────────────────────────────────"
                 "──────────────────────────────────────────\n\n"
                 "─────────────────────────────────────────────\n"
                 "Информация о графе\n"
                 "─────────────────────────────────────────────\n";
    if (!graph.Exist) {
        std::cout << "> Граф не существует, воспользуйтесь вводом.\n";
    } else  {
        // Печатаем основную информацию о графе.
        PrintGraphMainInfo(graph);
        cout << "Представление:\n";
        // Печатаем представление графа и информацию о его вершинах.
        PrintMatrixRepresentation(graph);
        PrintVertexDegrees(graph);
    }
    std::cout << "─────────────────────────────────────────────\n";
}

void PrintGraphMainInfo(const Graph &graph) {
    // Выводим основную информацию с учетом характеристик графа.
    if (graph.Pseudo)
        cout << "> [ОБНАРУЖЕН ПСЕВДОГРАФ] функционал ограничен.\n> [корректная работа не "
                "гарантируется]\n";
    else if (graph.Multi) {
        cout << "> [ОБНАРУЖЕН МУЛЬТИГРАФ] функционал ограничен.\n> [корректная работа не "
                "гаранитируется] \n";
    }
    cout << "> Тип представления графа: " << graph.type << ".\n";
    cout << "> Тип графа: " << (graph.Oriented ? "Ориентированный" : "Неориентированный")
    << ".\n";
    cout << "> Петли: " << (graph.WithLoops ? "Есть петли [корректная работа не "
                                              "гаранитруется]" : "Нет петель") << ".\n";
    if  (graph.WithLoops) {
        cout << "> Количество петель: " << graph.loops << "\n";
    }
    cout << "> Вершины: " << graph.p << ".\n";
    if (!graph.Oriented) {
        cout << "> " << "Рёбра: " << graph.q << ".\n";
    } else {
        cout << "> " << "Дуги: " << graph.arcs << ".\n";
    }
}

void PrintMatrixRepresentation(const Graph &graph) {
    // В зависимости от типа хранения графа, выводим его представление на экран.
    if (graph.type == "Матрица смежности") {
        string output = GetStringAdjacencyMatrix(graph);
        cout << output;
    } else if (graph.type == "Матрица инцидентности") {
        string output = GetStringIncidenceMatrix(graph);
        cout << output;
    } else if (graph.type == "Список смежности") {
        string output = GetStringAdjacencyList(graph);
        cout << output;
    } else if (graph.type == "Список ребер") {
        string output = GetStringRibsList(graph);
        cout << output;
    } else if (graph.type == "FO") {
        string output = GetStringFO(graph);
        cout << output;
    } else if (graph.type == "FI") {
        string output = GetStringFI(graph);
        cout << output;
    } else if (graph.type == "MFO") {
        string output = GetStringMFO(graph);
        cout << output;
    } else if (graph.type == "MFI") {
        string output = GetStringMFI(graph);
        cout << output;
    } else if (graph.type == "BFO") {
        string output = GetStringBFO(graph);
        cout << output;
    } else if (graph.type == "BMFO") {
        string output = GetStringBMFO(graph);
        cout << output;
    }
}

void PrintVertexDegrees(const Graph &graph) {
    // Если граф ориентрованный - выводим информацию о степенях вершины как о полустепенях.
    // Иначе выводим степени вершин.
    if (graph.Oriented) {
        string output;
        output += "Полустепени вершин:\n";
        output += " \tИсход\tЗаход\n";
        for (size_t i = 0; i < graph.vertex_degrees.size(); ++i) {
            output += to_string(i+1) + ": ";
            for (int j : graph.vertex_degrees[i]) {
                output += "\t" + to_string(j);
            }
            output += "\n" ;
        }
        cout << output;
    } else {
        string output;
        output += "Cтепени вершин:\n";
        for (size_t i = 0; i < graph.vertex_degrees.size(); ++i) {
            output += to_string(i+1) + ": ";
            for (int j : graph.vertex_degrees[i]) {
                output += to_string(j) + "";
            }
            output += "\n" ;
        }
        cout << output;
    }
}

void DoGraphWork(int32_t now_choice, Graph& graph) {
    // В зависимости от выбора пользователя, совершаем определённое действие.
    switch(now_choice)
    {
        case 1:
            GetNewGraph(graph);
            break;
        case 2:
            OutputGraph(graph);
            break;
        case 3:
            ChangeTypeOfGraph(graph);
            break;
        case 4:
            CountDegreesOfVertices(graph);
            break;
        case 5:
            CountEdgesOfGraph(graph);
            break;
        case 6:
            DFSClassic(graph);
            break;
        case 7:
            DFSRecursion(graph);
            break;
        case 8:
            BFS(graph);
            break;
        case 9:
            cout << ">>>>>>>>>>>>>>>>>>>>>\nВыходим из программы!\n";
            break;
        default:
            break;
    }
}

void GetNewGraph(Graph& graph) {
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к вводу графа!";
    std::cout << "\n─────────────────────────────────────────────\n";
    cout << "[1] Через консоль\n[2] Через файл Input.txt (В одной директории с CmakeList.txt)\n";
    std::cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(2);
    std::cout << "─────────────────────────────────────────────\n";
    if (choice == 1) {
        GetConsoleGraphInput(graph);
    } else {
        GetFileGraphInput(graph);
    }
}

void GetFileGraphInput(Graph &graph) {
    cout << "─────────────────────────────────────────────\nВвод будет производится через"
                 " файл.\nВыберем тип ввода!\n─────────────────────────────────────────────\n";
    cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n[4]"
            " Список ребер\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n[9] BMFO\n[10] BFO\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice_second = GetChoiceVarious(10);
    switch (choice_second) {
        case 1:
            GetAdjacencyMatrixFromFile(graph);
            break;
        case 2:
            GetIncidenceMatrixFromFile(graph);
            break;
        case 3:
            GetAdjacencyListFromFile(graph);
            break;
        case 4:
            GetRibsListFromFile(graph);
            break;
        case 5:
            GetFOFromFile(graph);
            break;
        case 6:
            GetFIFromFile(graph);
            break;
        case 7:
            GetMFOFromFile(graph);
            break;
        case 8:
            GetMFIFromFile(graph);
            break;
        case 9:
            GetBMFOFromFile(graph);
            break;
        case 10:
            GetBFOFromFile(graph);
            break;
        default:
            return;
    }
}

void GetBFOFromFile(Graph &graph) {
    vector<vector<int>> adjacency_matrix{};
    vector<int> bfo_fo{};
    string type = "BFO";
    int32_t number_of_strings = 1;
    cout << "Введите размер массива BFO:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива BFO");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, 0, 7)) {
                bfo_fo = final;
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    adjacency_matrix = ParseFromBFO(bfo_fo);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetBMFOFromFile(Graph &graph) {
    vector<int> bmfo_me{};
    vector<int> bmfo_mv{};
    cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\nВведите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    if (!TryGetInfoBMFOFromFile(number_of_colums, bmfo_me, bmfo_mv)) {
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, "BMFO");
}

bool TryGetInfoBMFOFromFile(int32_t number_of_colums, vector<int> &bmfo_me, vector<int> &bmfo_mv) {
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != 2) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        vector<int> final;
        vector<string> str = Split(string_data[0], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) bmfo_mv = final;
        else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        final = {};
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) bmfo_me = final;
        else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetMFIFromFile(Graph &graph) {
    vector<int> mfi_me{};
    vector<int> mfi_mv{};
    string type = "MFI";
    cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    if (!TryGetMFIFromFile(number_of_colums, mfi_me, mfi_mv)) {
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

bool TryGetMFIFromFile(int32_t number_of_colums, vector<int> &mfi_me, vector<int> &mfi_mv) {
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != 2) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        vector<int> final;
        vector<string> str = Split(string_data[0], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) {
            mfi_mv = final;
            final = {};
        } else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) {
            mfi_me = final;
            final = {};
        } else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetMFOFromFile(Graph &graph) {
    vector<int> mfo_me{};
    vector<int> mfo_mv{};
    string type = "MFO";
    cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    if (!TryGetMFOFromFile(number_of_colums, mfo_me, mfo_mv)) {
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

bool TryGetMFOFromFile(int32_t number_of_colums, vector<int> &mfo_me, vector<int> &mfo_mv) {
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != 2) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        vector<int> final;
        vector<string> str = Split(string_data[0], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) {
            mfo_mv = final;
            final = {};
        } else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(string_data, final, 0, 7)) {
            mfo_me = final;
            final = {};
        } else {
            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetFIFromFile(Graph &graph) {
    vector<vector<int>> adjacency_matrix{};
    vector<int> fi{};
    string type = "FI";
    int32_t number_of_strings = 1;
    cout << "Введите размер массива FI:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива FI");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, 0, 7)) {
                fi = final;
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    adjacency_matrix = ParseFromFI(fi);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetFOFromFile(Graph &graph) {
    vector<int> fo{};
    string type = "FO";
    int32_t  number_of_strings = 1;
    cout << "Введите размер массива FO:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива FO");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, 0, 7)) {
                fo = final;
                final = {};
            } else {
                cerr
                        << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    vector<vector<int>>  adjacency_matrix = ParseFromFO(fo);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetRibsListFromFile(Graph &graph) {
    vector<vector<int>> ribs_list{};
    string type = "Список ребер";
    cout << "Введите количество дуг/ребер для списка ребер:\n";
    int32_t  number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            int32_t number_of_colums = 2;
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, 1, 7)) {
                ribs_list.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromRibsList(ribs_list, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetAdjacencyListFromFile(Graph &graph) {
    vector<vector<int>> adjacency_list{};
    string type = "Список смежности";
    cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        int counter = 0;
        for (auto &i: string_data) {
            cout << "Введите количество смежных вершин для вершины." << ++counter << "\n";
            int32_t number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, 1, 7)) {
                adjacency_list.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromAdjacencyList(adjacency_list,number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, number_of_strings, adjacency_matrix, type);
}

void GetIncidenceMatrixFromFile(Graph &graph) {
    vector<vector<int>> incidence_matrix{};
    string type = "Матрица инцидентности";
    cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество ребер:\n";
    int32_t number_of_colums = GetChoiceVarious(50, "количество рёбер");
    cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != number_of_strings) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям.\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(string_data, final, -1, 1)) {
                incidence_matrix.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (exception &) {
        cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    auto adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    CheckAndSetFileData(graph, number_of_strings, adjacency_matrix, type);
}

void GetConsoleGraphInput(Graph &graph) {
    cout << "─────────────────────────────────────────────\nВвод будет производится через консоль."
            "\nВыберем тип ввода!\n─────────────────────────────────────────────\n[1] Матрица "
            "смежности\n[2] Матрица инцидентности\n[3] Список смежности\n[4] Список ребер\n[5] "
            "FO\n[6] FI\n[7] MFO\n[8] MFI\n[9] BMFO\n[10] BFO\n "
            "─────────────────────────────────────────────\n";
    switch (GetChoiceVarious(10)) {
        case 1:
            GetAdjacencyMatrixFromConsole(graph);
            break;
        case 2:
            GetIncidenceMatrixFromConsole(graph);
            break;
        case 3:
            GetAdjacencyListFromConsole(graph);
            break;
        case 4:
            GetRibsListFromConsole(graph);
            break;
        case 5:
            GetFOFromConsole(graph);
            break;
        case 6:
            GetFIFromConsole(graph);
            break;
        case 7:
            GetMFOFromConsole(graph);
            break;
        case 8:
            GetMFIFromConsole(graph);
            break;
        case 9:
            GetBMFOFromConsole(graph);
            break;
        case 10:
            GetBFOFromConsole(graph);
            break;
        default:
            break;
    }
}

void GetBFOFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> bfo_fo{};
    type = "BFO";
    number_of_strings = 1;
    cout << "Введите размер массива BFO:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива BFO");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                       "массива BFO");
        bfo_fo = matrix_line;
    }
    adjacency_matrix = ParseFromBFO(bfo_fo);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetBMFOFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> bmfo_me{};
    vector<int> bmfo_mv{};
    type = "BMFO";
    cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество элементов массива MV:\n";
    number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                       "массива MV");
        bmfo_mv = matrix_line;
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 1, 110,
                                           "массива ME");
        bmfo_me = matrix_line;
    }
    adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetMFIFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> mfi_me{};
    vector<int> mfi_mv{};
    type = "MFI";
    cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество элементов массива MV:\n";
    number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,"массива MV");
        mfi_mv = matrix_line;
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 1, 110, "массива ME");
        mfi_me = matrix_line;
    }
    adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetMFOFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> mfo_me{};
    vector<int> mfo_mv{};
    type = "MFO";
    cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество элементов массива MV:\n";
    number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    cout << "─────────────────────────────────────────────\n";
    {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,"массива MV");
        mfo_mv = matrix_line;
        cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 0, 110, "массива ME");
        mfo_me = matrix_line;
    }
    adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetFIFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> fi{};
    type = "FI";
    number_of_strings = 1;
    cout << "Введите размер массива FI:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива FI");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                       "массива FI");
        fi = matrix_line;
    }
    adjacency_matrix = ParseFromFI(fi);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetFOFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<int> fo{};
    type = "FO";
    number_of_strings = 1;
    cout << "Введите размер массива FO:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива FO");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                       "массива FO");
        fo = matrix_line;
    }
    adjacency_matrix = ParseFromFO(fo);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetRibsListFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<vector<int>> ribs_list{};
    cout << "Введите количество вершин:\n";
    number_of_colums = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    type = "Список ребер";
    cout << "Введите количество дуг/ребер для списка ребер:\n";
    number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        vector<int> matrix_line = GetMatrixLineVarious(2, 1, 7,
                                                       "списка ребер");
        ribs_list.push_back(matrix_line);
    }
    adjacency_matrix = ParseFromRibsList(ribs_list, number_of_colums);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyListFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<vector<int>> adjacency_list{};
    type = "Список смежности";
    cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        cout << "Введите количество смежных вершин для вершины " << i + 1 << "\n";
        number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                       "списка смежности");
        adjacency_list.push_back(matrix_line);
    }
    adjacency_matrix = ParseFromAdjacencyList(adjacency_list, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetIncidenceMatrixFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    vector<vector<int>> incidence_matrix{};
    type = "Матрица инцидентности";
    cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n";
    cout << "Введите количество ребер:\n";
    number_of_colums = GetChoiceVarious(50, "количество рёбер");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < number_of_strings; ++i) {
        vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, -1, 1,
                                                       "матрицы инцидентности");
        incidence_matrix.push_back(matrix_line);
    }
    adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyMatrixFromConsole(Graph &graph) {
    string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    vector<vector<int>> adjacency_matrix{};
    type = "Матрица смежности";
    cout << "Введите размер квадртаной матрицы смежности:\n";
    size_of_matrix = GetChoiceVarious(7, "размер матрицы");
    cout << "─────────────────────────────────────────────\n";
    for (int i = 0; i < size_of_matrix; ++i) {
        vector<int> matrix_line = GetMatrixLine(size_of_matrix);
        adjacency_matrix.push_back(matrix_line);
    }
    if (adjacency_matrix.empty()) {
        cerr << "Было введено недопустимое сочетание данных для представления!";
        return;
    }
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyMatrixFromFile(Graph &graph) {
    // Получаем матрицу смежности из файла с помощью построковой проверки входных данных.
    string type = "Матрица смежности";
    cout << "Введите размер квадртаной матрицы смежности в файле (должна точно совпадать):\n";
    int32_t size_of_matrix = GetChoiceVarious(7, "размер матрицы");
    cout << "─────────────────────────────────────────────\n";
    vector<vector<int>> adjacency_matrix;
    try {
        vector<string> string_data = ReadAllLinesInFile();
        if (string_data.size() != size_of_matrix) {
            cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto & i : string_data) {
            vector<int> final;
            vector<string> str = Split(i, ' ');
            if (str.size() != size_of_matrix) {
                cerr << "Размер одной из строк не соответствует измерениям квадратной матрицы"
                        ".\nНовый граф не установлен!\n";
                return;
            }
            // Проверяем валидна ли строка из файла для матрицы смежности.
            if (IsValidForMatrix(str, final)) {
                adjacency_matrix.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
        // Устанавливаем информацию о графе.
        CheckAndSetFileData(graph, size_of_matrix, adjacency_matrix, type);
    } catch (exception&) {
        cerr << "Произошла неизвестная ошибка при чтении из файла!\n";
        return;
    }
}

void CheckAndSetFileData(Graph &graph, int32_t size_of_matrix,
                         const vector<vector<int>> &adjacency_matrix, string type) {
    // Производим проверку матрицы смежности, чтобы она точно была подходящей для построения графа.
    if (!adjacency_matrix.empty() && adjacency_matrix.size() == size_of_matrix) {
        SetAdjacencyMatrix(graph, adjacency_matrix, std::move(type));
        cout << "Успех! Матрица установленна!\n";
    } else if (adjacency_matrix.empty()) {
        cerr << "Не удалось считать матрицу, она пуста или невалидна!\nНовый граф не "
                "установлен.\n";
    } else {
        cerr << "Ошибка размерности матрицы или она не подходит под формат!\nНовый граф не "
                "установлен.\n";
    }
}

vector<string> ReadAllLinesInFile() {
    // Cчитываем данные из файла.
    string line = ReadFileIntoString("..\\Input.txt");
    // Делим на строки по переводу строки.
    vector<string> string_data = SplitSegment(line);
    // Выводим содержимое файла на экран.
    cout << "<< Начало содержимого файла >>\n";
    int counter_strings = 1;
    for (auto & i : string_data) {
        cout<< counter_strings++ << "|\t" << i << endl;
    }
    cout << "<< Конец содержимого файла >>\n";
    return string_data;
}

void SetAdjacencyMatrix(Graph &graph, const vector<vector<int>>& adjacency_matrix, string type) {
    // Устанавливаем каждое представление в структуру графа, получаем всю информацию сразу.
    graph.adjacency_matrix = adjacency_matrix;
    graph.Exist = true;
    graph.Oriented = IsOriented(adjacency_matrix);
    graph.WithLoops = (CountLoops(adjacency_matrix) > 0);
    graph.Pseudo = IsPseudo(adjacency_matrix, graph.Oriented);
    graph.Multi = IsMulti(adjacency_matrix);
    graph.type = std::move(type);
    graph.p = static_cast<int>(adjacency_matrix.size());
    graph.q = CountRibs(adjacency_matrix);
    graph.arcs = CountArcs(adjacency_matrix);
    graph.loops = CountLoops(adjacency_matrix);
    graph.vertex_degrees = GetVertexDegrees(adjacency_matrix, graph.WithLoops, graph.Oriented);
    graph.ribs_list = ParseInRibsList(adjacency_matrix, graph.arcs);
    graph.adjacency_list = ParseInAdjacencyList(adjacency_matrix);
    graph.incidence_matrix = ParseInIncidenceMatrix(graph.adjacency_list);
    graph.fo = ParseInFO(adjacency_matrix, graph.Oriented);
    graph.fi = ParseInFI(adjacency_matrix, graph.Oriented);
    graph.mfo_me = ParseInMFO(adjacency_matrix, graph.Oriented).first;
    graph.mfo_mv = ParseInMFO(adjacency_matrix, graph.Oriented).second;
    graph.mfi_me = ParseInMFI(adjacency_matrix, graph.Oriented).first;
    graph.mfi_mv = ParseInMFI(adjacency_matrix, graph.Oriented).second;
    graph.bmfo_me = ParseInBMFO(adjacency_matrix, graph.Oriented).first;
    graph.bmfo_mv = ParseInBMFO(adjacency_matrix, graph.Oriented).second;
    graph.bfo_fo = ParseInBFO(adjacency_matrix, graph.Oriented);
}

void OutputGraph(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    // Справшиваем у пользователя предпочтительный стиль вывода.
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к выводу графа в текущем представлении";
    std::cout << "\n─────────────────────────────────────────────\n";
    cout << "[1] В консоль\n[2] В файл Output.txt (В одной директории с CmakeList.txt)\n";
    std::cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(2);
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    if (choice == 1) {
        // Печатаем текущее представление графа в консоль.
        PrintMatrixRepresentation(graph);
    } else {
        // Печатаем текущее представление графа в файл.
        try {
            char * fileName = const_cast<char *>("..\\Output.txt");
            FILE * file = fopen(fileName, "w");
            if (file)
            {
                string output = GetStringMatrixRepresentation(graph);
                const char * str = output.c_str();
                bool result = fputs(str, file);
                if (!result) cout << "Запись прошла успешно!\n";
            }
            else {
                cerr << "Нет доступа к файлу!\n";
            }
            fclose(file);
        } catch (exception& e) {
            cerr << "Произошла ошибка при записи в файл!\n";
        }
    }
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraph(Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к смене типа представления/хранения графа";
    std::cout << "\n─────────────────────────────────────────────\n";
    // В зависимости от типа графа можно предложить только определённые типы представления, для
    // этого пользователю предоставляется ограниченая возможность преобразования.
    if (graph.Pseudo && graph.Oriented) {
        ChangeTypeOfGraphForPseudoOriented(graph);
    } else if (graph.Pseudo && !graph.Oriented) {
        ChangeTypeOfGraphForPseudoNotOriented(graph);
    } else if (graph.Multi && !graph.Oriented) {
        ChangeTypeOfGraphForMulti(graph);
    } else if (!graph.Oriented) {
        ChangeTypeOfGraphForNotOriented(graph);
    } else {
        ChangeTypeOfGraphForOriented(graph);
    }
}

void ChangeTypeOfGraphForOriented(Graph &graph) {
    cout << "Для ориентированного графа доступны следующие представления.\n";
    cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n"
            "[4] Список ребёр\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(8);
    cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    switch (choice) {
        case 1 :
            graph.type = "Матрица смежности";
            break;
        case 2 :
            graph.type = "Матрица инцидентности";
            break;
        case 3 :
            graph.type = "Список смежности";
            break;
        case 4 :
            graph.type = "Список ребер";
            break;
        case 5 :
            graph.type = "FO";
            break;
        case 6 :
            graph.type = "FI";
            break;
        case 7 :
            graph.type = "MFO";
            break;
        case 8 :
            graph.type = "MFI";
            break;
        default:
            graph.type = "Матрица смежности";
            break;
    }
    cout << "Успех!\n";
    cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForNotOriented(Graph &graph) {
    cout << "Для неориентированного графа доступны следующие представления.\n";
    cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n"
            "[4] Список ребёр\n[5] FO\n[6] MFO\n[7] BFO\n[8] BMFO\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(8);
    cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    switch (choice) {
        case 1 :
            graph.type = "Матрица смежности";
            break;
        case 2 :
            graph.type = "Матрица инцидентности";
            break;
        case 3 :
            graph.type = "Список смежности";
            break;
        case 4 :
            graph.type = "Список ребер";
            break;
        case 5 :
            graph.type = "FO";
            break;
        case 6 :
            graph.type = "MFO";
            break;
        case 7 :
            graph.type = "BFO";
            break;
        case 8 :
            graph.type = "BMFO";
            break;
        default:
            graph.type = "Матрица смежности";
            break;
    }
    cout << "Успех!\n";
    cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForMulti(Graph &graph) {
    cout << "Для неориентированного мультиграфа доступны следующие представления.\n";
    cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n"
            "[4] FO\n[5] MFO\n[6] BFO\n[7] BMFO\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(8);
    cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    switch (choice) {
        case 1 :
            graph.type = "Матрица смежности";
            break;
        case 2 :
            graph.type = "Список смежности";
            break;
        case 3 :
            graph.type = "Список ребер";
            break;
        case 4 :
            graph.type = "FO";
            break;
        case 5 :
            graph.type = "MFO";
            break;
        case 6 :
            graph.type = "BFO";
            break;
        case 7 :
            graph.type = "BMFO";
            break;
        default:
            graph.type = "Матрица смежности";
            break;
    }
    cout << "Успех!\n";
    cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForPseudoNotOriented(Graph &graph) {
    cout << "Для неориентированного псеводографа доступны следующие представления.\n";
    cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n[4] FO\n[5] MFO\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(5);
    cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    switch (choice) {
        case 1 :
            graph.type = "Матрица смежности";
            break;
        case 2 :
            graph.type = "Список смежности";
            break;
        case 3 :
            graph.type = "Список ребер";
            break;
        case 4 :
            graph.type = "FO";
            break;
        case 5 :
            graph.type = "MFO";
            break;
        default:
            graph.type = "Матрица смежности";
            break;
    }
    cout << "Успех!\n";
    cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForPseudoOriented(Graph &graph) {
    cout << "Для ориентированного псеводографа доступны следующие представления.\n";
    cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n"
            "[4] FO\n[5] FI\n[6] MFO\n";
    cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(6);
    cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    switch (choice) {
        case 1 :
            graph.type = "Матрица смежности";
            break;
        case 2 :
            graph.type = "Список смежности";
            break;
        case 3 :
            graph.type = "Список ребер";
            break;
        case 4 :
            graph.type = "FO";
            break;
        case 5 :
            graph.type = "FI";
            break;
        case 6 :
            graph.type = "MFO";
            break;
        default:
            graph.type = "Матрица смежности";
            break;
    }
    cout << "Успех!\n";
    cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void CountDegreesOfVertices(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к подсчёту степеней/полустепеней графа\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // Печатаем информацию о вершинах графа.
    PrintVertexDegrees(graph);
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void CountEdgesOfGraph(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к подсчёту рёбер/дуг графа\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // В зависимости от типа графа возвращаем пользователю информацию о ребрах или о дугах.
    if (!graph.Oriented) {
        cout << "" << "Рёбра: " << graph.q << ".\n";
    } else {
        cout << "" << "Дуги: " << graph.arcs << ".\n";
    }
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void DFSClassic(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к обходу графа в глубину стандартно!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    stack<int> stack;
    vector<vector<int>> adjacency_matrix = graph.adjacency_matrix;  // Матрица смежности.
    vector<int> used(adjacency_matrix.size());  // Место для информации о вершинах графа.
    vector<int> path;  // Путь по вершинам.
    for (int & i : used) i = 0;
    stack.push(0);
    // Пока стэк не пустой, продолжаем работу.
    while (!stack.empty())
    {
        int now_point = stack.top();  // Получаем вершину.
        stack.pop();
        // Eсли вершина посещена, то пропускаем итерацию
        if (used[now_point] == 2) continue;
        if (path.empty()) cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else cout << "Путь: " << path[path.size()-1] << " --> " << now_point + 1 << "\n";
        cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = static_cast<int>(adjacency_matrix.size())-1; j >= 0; j--)
        {
            if (adjacency_matrix[now_point][j] == 1 && used[j] != 2)
            {
                cout << "Обнаружили смежную вершину " << j+1<< "\n";
                stack.push(j);
                used[j] = 1;
                path.push_back(now_point + 1);
            }
        }
        cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
    }
    cout << "Обход в глубину стандартным методом завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void DFSSearchRecursion(int32_t start, int32_t end, const vector<vector<int>>& matrix,
                        vector<int>& used, vector<int>& path)
{
    if (path.empty()) {
        cout << "Cтартовая точка: " << start + 1 << "\n";
    }
    cout << "Посетили вершину " << start + 1 << "\n";
    path.push_back(start + 1);
    cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
    used[start] = 1;
    for (int i = 0; i < end; i++)
        // Если вершина еше не посешена, идем в неё
        if (matrix[start][i] != 0 && used[i] == 0) DFSSearchRecursion(i, end, matrix, used, path);
}

void DFSRecursion(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к обходу графа в глубину рекурсивно!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    vector<int> used(graph.adjacency_matrix.size());
    vector<int> path;
    for (int & i : used) i = 0;
    // Запускаем рекурсивный проход по врешинам.
    DFSSearchRecursion(0, static_cast<int>(graph.adjacency_matrix.size()), graph.adjacency_matrix,
                       used, path);
    cout << "Обход в глубину рекурсивным методом завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void BFS(const Graph& graph) {
    if (!graph.Exist) {
        cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к обходу графа в ширину!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    queue<int> queue;
    vector<vector<int>> adjacency_matrix = graph.adjacency_matrix;
    vector<int> used(adjacency_matrix.size()); // Вектор для ифнормации по вершинам графа.
    vector<int> path;
    for (int & i : used) i = 0;
    queue.push(0);
    while (!queue.empty())
    {
        int now_point = queue.front(); // Получаем вершину.
        queue.pop();
        if (used[now_point] == 2) continue;
        if (path.empty()) cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else cout << "Путь: " << path[path.size()-1] << " --> " << now_point + 1 << "\n";
        cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = 0; j < adjacency_matrix.size(); j++)
        {
            // Если вершина не посещена, то выполняем обнаружение.
            if (adjacency_matrix[now_point][j] == 1 && used[j] != 2)
            {
                cout << "Обнаружили смежную вершину " << j+1<< "\n";
                queue.push(j);
                used[j] = 1;
                path.push_back(now_point + 1);
            }
        }
        cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n"; // выводим номер вершины
    }
    cout << "Обход в ширину завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

bool IsOriented(vector<vector<int>> matrix) {
    // Если окажется, что одно из ребер не введет из одной вершину в другую и обратно, то граф
    // ориентированный.
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix[i].size(); ++j) {
            if (i != j && matrix[i][j] != matrix[j][i]) {
                return true;
            }
        }
    }
    return false;
}

bool IsMulti(const vector<vector<int>> &matrix) {
    // Если окажется, что хоть одино ребро не одно или из одной врешщины в другую введет два
    // ребра, мы говорим, что это мультиграф.
    for (auto & i : matrix) {
        for (int j : i) {
            if (j > 1){
                return true;
            }
        }
    }
    return false;
}

bool IsPseudo(const vector<vector<int>>& matrix, bool oriented) {
    // Если граф неориентированный, то он не псевдограф, однако если граф ориентированный и хоть
    // одина дуга повторятеся, это псевдограф.
    if (!oriented) return false;
    for (auto & i : matrix) {
        for (int j : i) {
            if (j > 1){
                return true;
            }
        }
    }
    return false;
}

int CountLoops(vector<vector<int>> matrix) {
    // Считаем петли проходясь по ребрам, которые ведут из вершины в сами себя.
    int count = 0;
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix[i].size(); ++j) {
            if (i == j) {
                if (matrix[i][j] > 0) {
                    count += matrix[i][j];
                }
            }
        }
    }
    return count;
}

int CountArcs(const vector<vector<int>>& matrix) {
    // Считаем дуги с помощью подсчета одностороних путей из одной вершины в другу не без вершин
    // посредников.
    int count = 0;
    for (auto & i : matrix) {
        for (int j : i) {
            if (j > 0) {
                count += j;
            }
        }
    }
    return count;
}

int CountRibs(vector<vector<int>> matrix) {
    // Считаем все ребра с учетом петлей и ребер;
    int count = 0;
    int anti_count = 0;
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix[i].size(); ++j) {
            if (matrix[i][j] > 0 && matrix[j][i] == matrix[i][j] & i != j) {
                count += matrix[i][j];
                anti_count += matrix[i][j];
            } else if (matrix[i][j] > 0) {
                count += matrix[i][j];
            }
        }
    }
    return count - static_cast<int>(floor(static_cast<double>(anti_count)/2.0));
}

vector<vector<int>> GetVertexDegrees(vector<vector<int>> matrix, bool with_loops, bool oriented) {
    // Получаем представление о степенях вершин с учетом характеристик графа.
    vector<vector<int>> vertex_degrees(matrix.size());
    if (!with_loops && !oriented) {
        for (size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
    } else if (with_loops && !oriented) {
        for (size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            int32_t loops = 0;
            for (size_t j = 0; j < matrix[i].size(); ++j) {
                if (i == j) {
                    loops += matrix[i][j];
                    sum += matrix[i][j];
                } else {
                    sum += matrix[i][j];
                }
            }
            vertex_degrees[i].push_back(sum+loops);
        }
    } else {
        for (size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
        vector<vector<int>> new_matrix(matrix.size());
        for (size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                        .push_back(j[i]);
        for (size_t i = 0; i < new_matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : new_matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
    }
    return vertex_degrees;
}

vector<vector<int>> ParseInIncidenceMatrix(const vector<vector<int>>& adjacency_list) {
    std::vector<std::vector<int>> incidence;
    std::set<std::pair<int, int>> rejected;
    // Без повторений фиксируем смежность вершин в матрице инцинденций.
    for (size_t  i = 0; i < adjacency_list.size(); ++i) {
        for (size_t  j = 0; j < adjacency_list[i].size(); ++j) {
            if (!rejected.contains({i, adjacency_list[i][j]-1})) {
                std::vector<int> work_flow(adjacency_list.size());
                auto pointer = find(adjacency_list[adjacency_list[i][j]-1].begin(),
                                    adjacency_list[adjacency_list[i][j]-1].end(), i+1);
                // Если есть путь туда, то фиксируем направление.
                if (pointer == adjacency_list[adjacency_list[i][j]-1].end()) {
                    int path = static_cast<int>(std::count(
                            adjacency_list[adjacency_list[i][j]-1].begin(),
                            adjacency_list[adjacency_list[i][j]-1].end(), i+1));
                    work_flow[i] = 1;
                    work_flow[adjacency_list[i][j]-1] = -1;
                } else {
                    // Если есть путь туда и обратно, помечаем вершины как смежные по ребру.
                    work_flow[i] = 1;
                    work_flow[adjacency_list[i][j]-1] = 1;
                }
                rejected.insert({adjacency_list[i][j]-1, i});
                incidence.push_back(work_flow);
            }
        }
    }
    // Транспонируем конечную матрицу.
    std::vector<std::vector<int>> new_matrix(adjacency_list.size());
    for (size_t i = 0; i < adjacency_list.size(); ++i){
        for (auto & j : incidence) {
            new_matrix[i].push_back(j[i]);
        }
    }
    return new_matrix;
}

vector<vector<int>> ParseInAdjacencyList(const vector<vector<int>>& matrix) {
    // Фиксируем в виде векторов смежные вершины, проходясь по матрице по строкам.
    std::vector<std::vector<int>> adjacency_list(matrix.size());
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    adjacency_list[i].push_back(static_cast<int>(j)+1);
                    --num_of_path;
                }
            }
        }
    }
    return adjacency_list;
}

vector<vector<int>> ParseInRibsList(const vector<vector<int>>& matrix, int32_t arcs) {
    // Получаем из матрицы рёбра, каждый раз выделяя каждый путь в отдельный вектор из двух
    // элементов: конец и начало.
    std::vector<std::vector<int>> ribs_list(arcs);
    int32_t ribs_count = 0;
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    ribs_list[ribs_count].push_back(static_cast<int>(i)+1);
                    ribs_list[ribs_count].push_back(static_cast<int>(j)+1);
                    ++ribs_count;
                    --num_of_path;
                }
            }
        }
    }
    return ribs_list;
}

vector<int> ParseInFO(const vector<vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, смежные с текущей вершиной, деля их 0.
    std::vector<int> fo;
    fo.push_back(static_cast<int>(matrix.size()));
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    fo.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
            }
        }
        fo.push_back(0);
    }
    return fo;
}

vector<int> ParseInFI(const vector<vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, входящие в текущую вершину, деля их
    // 0-разделителем.
    std::vector<int> fi;
    if (!oriented) {
        return {};
    }
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    fi.push_back(static_cast<int>(new_matrix.size()));
    for (size_t i = 0; i < new_matrix.size(); ++i) {
        for (size_t j = 0; j < new_matrix.size(); ++j) {
            if (new_matrix[i][j] > 0) {
                int num_of_path = new_matrix[i][j];
                while (num_of_path != 0) {
                    fi.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
            }
        }
        fi.push_back(0);
    }
    return fi;
}

pair<vector<int>, vector<int>> ParseInMFO(const vector<vector<int>>& matrix, bool oriented) {
    // Производим разбиение на два вектора, в первом мы записываем смежные вершины (как в FO) без
    // разделителя, а в массив mv описывает секции выделяя номер (начиная с 1) последней смежной
    // с этой вершиной вершины.
    std::vector<int> me;
    std::vector<int> mv;
    int last = 0;
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    me.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
                last = static_cast<int>(me.size());
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}

pair<vector<int>, vector<int>> ParseInMFI(const vector<vector<int>>& matrix, bool oriented){
    // Производим разбиение на два вектора, в первом мы записываем смежные вершины (как в FI) без
    // разделителя, а в массив mv описывает секции выделяя номер (начиная с 1) первой смежной
    // с этой вершиной вершины.
    if (!oriented) {
        return {{}, {}};
    }
    std::vector<int> me(0);
    std::vector<int> mv(0);
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    int last = 0;
    for (size_t i = 0; i < new_matrix.size(); ++i) {
        for (size_t j = 0; j < new_matrix.size(); ++j) {
            if (new_matrix[i][j] > 0) {
                int num_of_path = new_matrix[i][j];
                while (num_of_path > 0) {
                    me.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
                last = static_cast<int>(me.size());
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}

vector<int> ParseInBFO(const vector<vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, смежные с текущей вершиной, но с номером не
    // ниже текущей вершины, деля их 0.
    if (oriented) {
        return {};
    }
    std::vector<int> fo;
    fo.push_back(static_cast<int>(matrix.size()));
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    if (j >= i) {
                        fo.push_back(static_cast<int>(j) + 1);
                    }
                    --num_of_path;
                }
            }
        }
        fo.push_back(0);
    }
    return fo;
}

pair<vector<int>, vector<int>> ParseInBMFO(const vector<vector<int>>& matrix, bool oriented) {
    // Производим разбиение на два вектора, в первом мы записываем смежные вершины (как в FO,
    // однако используем принцип из BFO) без
    // разделителя, а в массив mv описывает секции выделяя номер (начиная с 1) последней смежной
    // с этой вершиной вершины.
    std::vector<int> me;
    std::vector<int> mv;
    int last = 0;
    for (size_t i = 0; i < matrix.size(); ++i) {
        for (size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    if (j >= i) {
                        me.push_back(static_cast<int>(j) + 1);
                    }
                    --num_of_path;
                }
                last = static_cast<int>(me.size());
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}

string GetStringMatrixRepresentation(const Graph &graph) {
    // В зависимости от типа хранения возфращаем представление информации о графе в качестве строки.
    string output;
    if (graph.type == "Матрица смежности") {
        output = GetStringAdjacencyMatrix(graph);
    } else if (graph.type == "Матрица инцидентности") {
        output = GetStringIncidenceMatrix(graph);
    } else if (graph.type == "Список смежности") {
        output = GetStringAdjacencyList(graph);
    } else if (graph.type == "Список ребер") {
        output = GetStringRibsList(graph);
    } else if (graph.type == "FO") {
        output = GetStringFO(graph);
    } else if (graph.type == "FI") {
        output = GetStringFI(graph);
    } else if (graph.type == "MFO") {
        output = GetStringMFO(graph);
    } else if (graph.type == "MFI") {
        output = GetStringMFI(graph);
    } else if (graph.type == "BFO") {
        output = GetStringBFO(graph);
    } else if (graph.type == "BMFO") {
        output = GetStringBMFO(graph);
    }
    return output;
}

string GetStringBMFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.bmfo_me) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    output += "MV: [";
    check = 0;
    for (int i : graph.bmfo_mv) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
}

string GetStringBFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "BFO: [";
    int check = 0;
    for (int i : graph.bfo_fo) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n";
    return output;
}

string GetStringMFI(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.mfi_me) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    output += "MV: [";
    check = 0;
    for (int i : graph.mfi_mv) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
}

string GetStringMFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.mfo_me) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    check = 0;
    output += "MV: [";
    for (int i : graph.mfo_mv) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
}

string GetStringFI(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "FI: [";
    int check = 0;
    for (int i : graph.fi) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    return output;
}

string GetStringFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    output += "FO: [";
    int check = 0;
    for (int i : graph.fo) {
        output += to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    return output;
}

string GetStringRibsList(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    string output;
    output += " \tНачало\tКонец\n";
    for (size_t i = 0; i < graph.ribs_list.size(); ++i) {
        output += to_string(i+1) + ": ";
        for (int j : graph.ribs_list[i]) {
            output += "\t" + to_string(j);
        }
        output += "\n" ;
    }
    return output;
}

string GetStringAdjacencyList(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    string output;
    for (size_t i = 0; i < graph.adjacency_list.size(); ++i) {
        output += to_string(i+1) + ": ";
        int check = 0;
        for (int j : graph.adjacency_list[i]) {
            output += to_string(j) + ",";
            ++check;
        }
        if (check != 0) output = output.substr(0, output.size()-1);
        output += "\n" ;
    }
    return output;
}

string GetStringIncidenceMatrix(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    string output = "[НУМЕРАЦИЯ РЁБЕР СОГЛАСНО МАТРИЦЕ СМЕЖНОСТИ]\n";
    for (int i = 0; i < graph.q; ++i) {
        output += "\t" + to_string(i+1);
    }
    output += "\n";
    for (int i = 0; i < graph.p; ++i) {
        output += to_string(i + 1);
        for (int j = 0; j < graph.q; ++j) {
            output += "\t" + to_string(graph.incidence_matrix[i][j]);
        }
        output += "\n" ;
    }
    return output;
}

string GetStringAdjacencyMatrix(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    string output;
    for (int i = 0; i < graph.p; ++i) {
        output += "\t" + to_string(i+1);
    }
    output += "\n";
    for (int i = 0; i < graph.p; ++i) {
        output += to_string(i + 1);
        for (int j = 0; j < graph.p; ++j) {
            output +=  + "\t" + to_string(graph.adjacency_matrix[i][j]);
        }
        output += "\n" ;
    }
    return output;
}

int32_t GetChoice() {
    bool valid_choice = false;
    int32_t ans = 0;
    std::string line;
    std::cout << "Выбретите опцию, отправив число от 1 до 9.\n";
    // Пока не будет введено подходящее число, продолжаем получать строки от пользователя.
    do {
        try {
            std::getline(std::cin, line);
            // Пытаемся преобразовать строку в число.
            ans = stoi(line);
        } catch (std::exception& exception){
            // Возвращаем консоль в хорошее состояние после ошибки.
            std::cin.clear();
        }
        // Проверяем диапазон, в котором должно лежать число.
        if (ans >= 1 && ans <= 9) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до 9!\n";
        }
    } while (!valid_choice);
    system("color 0B");
    return ans;
}

int32_t GetChoiceVarious(int32_t upper_bound) {
    bool valid_choice = false;
    int32_t ans = 0;
    string line;
    cout << "Выбретите опцию, отправив число от 1 до "<< upper_bound << ".\n";
    // Пока не будет введено подходящее число, продолжаем получать строки от пользователя.
    do {
        try {
            std::getline(std::cin, line);
            ans = stoi(line);
        } catch (std::exception& exception){
            // Возвращаем консоль в хорошее состояние после ошибки.
            std::cin.clear();
        }
        // Проверяем диапазон, в котором должно лежать число.
        if (ans >= 1 && ans <= upper_bound) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до "<< upper_bound << "!\n";
        }
    } while (!valid_choice);
    system("color 0B");
    return ans;
}

int32_t GetChoiceVarious(int32_t upper_bound, const string& info) {
    bool valid_choice = false;
    int32_t ans = 0;
    std::string line;
    std::cout << "Выбретите " << info << ", отправив число от 1 до "<< upper_bound << ".\n";
    // Пока не будет введено подходящее число, продолжаем получать строки от пользователя.
    do {
        try {
            std::getline(std::cin, line);
            ans = stoi(line);
        } catch (std::exception& exception){
            // Возвращаем консоль в хорошее состояние после ошибки.
            std::cin.clear();
        }
        // Проверяем диапазон, в котором должно лежать число.
        if (ans >= 1 && ans <= upper_bound) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до "<< upper_bound << "!\n";
        }
    } while (!valid_choice);
    system("color 0B");
    return ans;
}

int32_t ParseNum(const string& s) {
    int32_t ans = 0;
    try {
        // Производим попытку преобразовать строку к числу.
        ans = stoi(s);
        // Проверяем диапазон.
        if (ans >= 1 && ans <= 10) {
            return ans;
        } else {
            return 0;
        }
    } catch (std::exception& exception){
        return 0;
    }
    return ans;
}

ifstream::pos_type GetFileSize(const string& filename)
{
    // Получаем длину файла в байтах, в симолах стандартной кодировки.
    std::ifstream in(filename, std::ifstream::ate | std::ifstream::binary);
    return in.tellg();
}

string ReadFileIntoString(const string& path) {
    ifstream input_file(path);
    try {
        int size = 0;
        // Получаем размер файла
        size = static_cast<int>(GetFileSize(path));
        // Провреяем размер файла.
        if (size > 30000) {
            cerr << "Отмена чтения. Файл весит больше 30 КБ!\n";
            return "";
        }
        // Получилось ли открыть файл.
        if (!input_file.is_open()) {
            cerr << "Невозможно открыть файл!\n";
            return "";
        }
        // Возвращаем всю строку из файла обратно.
        return {string((std::istreambuf_iterator<char>(input_file)),
                      std::istreambuf_iterator<char>())};
    } catch (exception&) {
        cerr << "Произошла ошибка при чтении из файла!\n";
        input_file.close();
    }
    return "";
}

vector<int> GetMatrixLine(int32_t size_of_matrix) {
    string line;
    bool flag = false;
    vector<int> final;
    vector<string> string_data;
    do {
        cout << "Введите строку матрицы размером " << size_of_matrix << endl;
        try {
            getline(cin, line);
            // Проверяем символ.
            int check = stoi(line);
            // Разделяем строку по пробелам.
            string_data = Split(line, ' ');
            // Проверяем размер строки.
            if (string_data.size() != size_of_matrix) {
                cout << "Размер не подходит!\n";
            } else {
                // Проверяем введеную строку на валидность.
                if (IsValidForMatrix(string_data, final)) {
                    cout << "Успешно получена строка матрицы!\n";
                    return final;
                }
            }
        } catch (exception&) {
            cout << "Введите подходящую строку!\n";
            // Возвращаем валидное значение строки.
            std::cin.clear();
            continue;
        }
    } while (!flag);
    return final;
}

vector<string> Split(const string& str, char delim) {
    vector<string> ans;
    try {
        std::stringstream str_stream(str);
        string item;
        // Получаем элементы через текущий разделитель и если это не пустые строки, заносим в
        // вектор.
        while (std::getline(str_stream, item, delim)) {
            if (item[0] != delim && !item.empty()) ans.push_back(item);
        }
        return ans;
    } catch (exception&) {
        cerr << "Не вводите пустую строку!\n";
        // При ошибке также очищаем консоль, возвращая ее в стандартное состояние.
        cin.clear();
    }
    return ans;
}

vector<string> SplitSegment(const string& sentence) {
    vector<string> ans;
    try {
        std::stringstream str_stream(sentence);
        std::string item;
        // Получаем элементы через перенос строки и если это не пустые строки, заносим в
        // вектор.
        while (std::getline(str_stream, item, '\n')) {
            if (item != " " && !item.empty()) ans.push_back(item);
        }
    } catch (exception&) {
        cerr << "Возникла ошибка при разделении файла на строки!\n";
    }
    return ans;
}

bool IsValidString(const string& s) {
    int32_t ans = 0;
    try {
        // Пробуем произвести приведение строки к числу.
        ans = stoi(s);
        // Проверяем диапазон.
        if (ans >= 0 && ans <= 4) {
            return true;
        } else {
            cerr << "Одно из чисел не в пределах от 0 до 4!\n";
            return false;
        }
    } catch (exception&){
        cerr << "Данные не содержат число!\n";
        return false;
    }
    return ans;
}

bool IsValidForMatrix(const vector<string>& line, vector<int>& ans){
    for (auto & i : line) {
        // Проверяем, является ли строка подходящей для представления.
        if (!IsValidString(i)) {
            ans = {};
            return false;
        } else {
            // Если да, то добавляем уже преобразованную в число строку в конечный вектор.
            ans.push_back(ParseNum(i));
        }
    }
    return true;
}

bool IsValidString(const string& s, int32_t lower_bound, int32_t upper_bound) {
    int32_t ans = 0;
    try {
        // Пытаемся преобразовать строку к числу.
        ans = stoi(s);
        // Проверяем диапазон.
        if (ans >= lower_bound && ans <= upper_bound) {
            return true;
        } else {
            cerr << "Одно из чисел не в пределах от "<< lower_bound << " до " << upper_bound << "!\n";
            return false;
        }
    } catch (std::exception& exception){
        cerr << "Данные не содержат число!\n";
        return false;
    }
    return ans;
}

int32_t ParseNum(const string& s, int32_t lower_bound, int32_t upper_bound) {
    int32_t ans = 0;
    try {
        // Производим попытку преобразовать строку к числу.
        ans = stoi(s);
        // Проверяем диапазон.
        if (ans >= lower_bound && ans <= upper_bound) {
            return ans;
        } else {
            return 0;
        }
    } catch (std::exception& exception){
        return 0;
    }
    return ans;
}

bool IsValidForMatrix(const vector<string>& line, vector<int>& ans, int32_t lower_bound,
                      int32_t upper_bound) {
    for (auto & i : line) {
        // Проверяем, является ли строка подходящей для представления.
        if (!IsValidString(i, lower_bound, upper_bound)) {
            ans = {};
            return false;
        } else {
            // Если да, то добавляем уже преобразованную в число строку в конечный вектор.
            ans.push_back(ParseNum(i, lower_bound, upper_bound));
        }
    }
    return true;
}

vector<int> GetMatrixLineVarious(int32_t size_of_matrix, int32_t lower_bound, int32_t upper_bound,
                          const string& comment) {
    string line;
    bool flag = false;
    vector<int> final;
    vector<string> string_data;
    do {
        cout << "Введите строку " << comment << " размером " << size_of_matrix << endl;
        try {
            // Получаем строку текущего представления.
            getline(cin, line);
            // Производим попытку преобразования первого символа в число.
            int check = stoi(line);
            // Делим строку по пробелам.
            string_data = Split(line, ' ');
            // Проверяем размер.
            if (string_data.size() != size_of_matrix) {
                cout << "Размер не подходит!\n";
            } else {
                // Если получаем подходящую строку - возвращаем.
                if (IsValidForMatrix(string_data, final, lower_bound, upper_bound)) {
                    cout << "Успешно получена строка " << comment << "!\n";
                    return final;
                }
            }
        } catch (exception& e) {
            cout << "Введите подходящую строку!\n";
            // При ошибке отчищаем консоль до последнего приемлемого состояния.
            std::cin.clear();
            continue;
        }
    } while (!flag);
    return final;
}

int Find(vector<int> &v, int a) {
    for (int i = 0; i < v.size(); i++) {
        if ( v[i] == a) {
            return i;
        }
    }
    return -1;
}

vector<vector<int>> ParseFromAdjacencyList(vector<vector<int>> adjacency_list, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    for (int i = 0; i < vertices; ++i) {
        for (int j = 0; j < vertices; ++j) {
            if (j < adjacency_list[i].size()) {
                if (adjacency_list[i][j] > vertices) {
                    cout << "Предупреждение: обнаружена вершина "<< adjacency_list[i][j] <<
                         " в списке, которая не покрыта"
                         " количеством вершин, она проигнорирована!\n";
                } else {
                    adjacency_matrix[i][adjacency_list[i][j]-1] = 1;
                }
            } else {
            }
        }
    }
    return adjacency_matrix;
}

vector<vector<int>> ParseFromIncidenceMatrix(vector<vector<int>> incidence_matrix, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    vector<vector<int>> matrix(incidence_matrix[0].size());
    for (size_t i = 0; i < incidence_matrix[0].size(); i++)
        for (auto & j : incidence_matrix) matrix[i].push_back(j[i]);
    for (auto & i : matrix) {
        if  (find(i.begin(), i.end(), 1) != i.end()) {
            if (count(i.begin(), i.end(), 1) == 2 && count(i.begin(), i.end(), -1) == 0) {
                auto find_one = Find(i, 1);
                i[find_one] = 0;
                auto find_two = Find(i, 1);
                if (find_one != -1 && find_two != -1) {
                    adjacency_matrix[find_one][find_two] = 1;
                    adjacency_matrix[find_two][find_one] = 1;
                } else {
                    cerr << "Неправильная конфигурация одного из ребер!\n";
                    return {};
                }
            } else if ((std::count(i.begin(), i.end(), 1) == 1)) {
                if  ((std::count(i.begin(), i.end(), -1) == 1)) {
                    auto find_one = Find(i, 1);
                    auto find_two = Find(i, -1);
                    if (find_two != -1 && find_one != -1) adjacency_matrix[find_one][find_two] = 1;
                    else {
                        cerr << "Неправильная конфигурация одного из ребер!\n";
                        return{};
                    }
                } else {
                    cerr << "Возникла ошибка, в одном из ребер не нашлось входящей вершины или"
                            " их слишком много!\n";
                    return {};
                }
            } else {
                cerr << "Возникла ошибка, в одном из ребер не нашлось выходящей вершины или"
                        " их слишком много!\n";
                return {};
            }
        } else {
            cerr << "Возникла ошибка, одно из ребер не может существовать!\n";
            return {};
        }
    }
    return adjacency_matrix;
}

vector<vector<int>> ParseFromRibsList(vector<vector<int>> ribs_list, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;

    for (auto & i : ribs_list) {
        for (int j = 0; j < 1; ++j) {
            if (i[j] > vertices) {
                cout << "Предупреждение: обнаружена вершина "<< i[j] <<
                     " в списке, которая не покрыта"
                     " количеством вершин, она проигнорирована!\n";
            } else if (i[j+1] > vertices) {
                cout << "Предупреждение: обнаружена вершина "<< i[j+1] <<
                     " в списке, которая не покрыта"
                     " количеством вершин, она проигнорирована!\n";
            } else  {
                adjacency_matrix[i[j]-1][i[j+1]-1] = 1;
            }
        }
    }

    return adjacency_matrix;
}

vector<vector<int>> ParseFromFO(vector<int> fo) {
    int counter = static_cast<int>(count(fo.begin(), fo.end(), 0));
    if (counter != fo[0]) {
        cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    vector<vector<int>> adjacency_matrix(counter);
    vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fo.size(); ++i) {
        if (fo[i] == 0) {
            for (int j : vertex) {
                if (j > counter) {
                    cerr << "Встречена вершина, которая не может находится в данном представлении!\n";
                    return {};
                }
                adjacency_matrix[count][j] = 1;
            }
            ++count;
            vertex= {};
        } else {
            vertex.push_back(fo[i]-1);
        }
    }
    return adjacency_matrix;
}

vector<vector<int>> ParseFromFI(vector<int> fi) {
    int counter = static_cast<int>(count(fi.begin(), fi.end(), 0));
    if (counter != fi[0]) {
        cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    vector<vector<int>> adjacency_matrix(counter);
    vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fi.size(); ++i) {
        if (fi[i] == 0) {
            for (int j : vertex) {
                if (j > counter) {
                    cerr << "Встречена вершина, которая не может находится в данном представлении!\n";
                    return {};
                }
                adjacency_matrix[count][j] = 1;
            }
            ++count;
            vertex= {};
        } else {
            vertex.push_back(fi[i]-1);
        }
    }
    vector<vector<int>> matrix(adjacency_matrix.size());
    for (size_t i = 0; i < adjacency_matrix.size(); i++)
        for (auto & j : adjacency_matrix) matrix[i].push_back(j[i]);
    return matrix;
}

vector<vector<int>> ParseFromMFO(const vector<int>& mv, vector<int> me, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    if  (mv.size() != vertices) {
        cerr << "Для графа, у которого свзяаны все вершины, размерность MV должна совпадать"
                " с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        if  (i == 0) continue;
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Встречена вершина,"
                                " которая не может находится в данном представлении!\n";
                        return {};
                    }
                    adjacency_matrix[count][k-1] = 1;
                }
                now_position = j+1;
                count += 1;
                vertex = {};
                break;
            } else {
                vertex.push_back(me[j]);
            }
        }
    }
    return adjacency_matrix;
}

vector<vector<int>> ParseFromMFI(const vector<int>& mv, vector<int> me, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    if  (mv.size() != vertices) {
        cerr << "Для графа, у которого свзяаны все вершины, размерность MV должна совпадать"
                " с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Встречена вершина,"
                                " которая не может находится в данном представлении!\n";
                        return {};
                    }
                    adjacency_matrix[count][k-1] = 1;
                }
                now_position = j+1;
                count += 1;
                vertex = {};
                break;
            } else {
                vertex.push_back(me[j]);
            }
        }
    }
    vector<vector<int>> matrix(adjacency_matrix.size());
    for (size_t i = 0; i < adjacency_matrix.size(); i++)
        for (auto & j : adjacency_matrix) matrix[i].push_back(j[i]);
    return matrix;
}

vector<vector<int>> ParseFromBMFO(const vector<int>& mv, vector<int> me, int32_t vertices) {
    vector<vector<int>> adjacency_matrix(vertices);
    vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    if  (mv.size() != vertices) {
        cerr << "Для графа, у которого свзяаны все вершины, размерность MV должна совпадать"
                " с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        if  (i == 0) continue;
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Встречена вершина,"
                                " которая не может находится в данном представлении!\n";
                        return {};
                    }
                    adjacency_matrix[count][k-1] = 1;
                    adjacency_matrix[k-1][count] = 1;
                }
                now_position = j+1;
                count += 1;
                vertex = {};
                break;
            } else {
                vertex.push_back(me[j]);
            }
        }
    }
    return adjacency_matrix;
}

vector<vector<int>> ParseFromBFO(vector<int> fo) {
    int counter = static_cast<int>(count(fo.begin(), fo.end(), 0));
    if (counter != fo[0]) {
        cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    vector<vector<int>> adjacency_matrix(counter);
    vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fo.size(); ++i) {
        if (fo[i] == 0) {
            for (int j : vertex) {
                if (j > counter) {
                    cerr << "Встречена вершина, которая не может находится"
                            " в данном представлении!\n";
                    return {};
                }
                adjacency_matrix[count][j] = 1;
                adjacency_matrix[j][count] = 1;
            }
            ++count;
            vertex= {};
        } else {
            vertex.push_back(fo[i]-1);
        }
    }
    return adjacency_matrix;
}

