#include "Functions.h"

// Укажем использование пространства имен std.
using namespace std;

struct Graph {
    // Существует ли граф.
    bool Exist = false;
    // Ориентированный ли граф.
    bool Oriented = false;
    // C петлями ли он.
    bool WithLoops = false;
    // Псевдограф или нет.
    bool Pseudo = false;
    // Мультиграф или нет.
    bool Multi = false;
    // Ориентированный ли граф.
    std::string type{};
    // Количество вершин.
    int32_t p = 0;
    // Количество рёбер.
    int32_t q = 0;
    // Количество дуг.
    int32_t arcs = 0;
    // Количество петель.
    int32_t loops = 0;
    // Cтепени или полустепени вершин.
    std::vector<std::vector<int>> vertex_degrees{};
    // Матрица смежности.
    std::vector<std::vector<int>> adjacency_matrix{};
    // Матрица инцидентности.
    std::vector<std::vector<int>> incidence_matrix{};
    // Список смежности
    std::vector<std::vector<int>> adjacency_list{};
    // Cписок ребер.
    std::vector<std::vector<int>> ribs_list{};
    // FO представление.
    std::vector<int> fo{};
    // FI представление.
    std::vector<int> fi{};
    // MFO представление, вектор ME.
    std::vector<int> mfo_me{};
    // MFO представление, вектор MV.
    std::vector<int> mfo_mv{};
    // MFI представление, вектор ME.
    std::vector<int> mfi_me{};
    // MFI представление, вектор MV.
    std::vector<int> mfi_mv{};
    // BMFO представление, вектор ME.
    std::vector<int> bmfo_me{};
    // BMFO представление, вектор MV.
    std::vector<int> bmfo_mv{};
    // BFO представление.
    std::vector<int> bfo_fo{};
};

void ShowStartOfProgram() {
    // Выводим заставку на экран.
    std::cout << "Это заставка\n";
}

void ShowExitOfProgram() {
    // Выводим прощание на экран.
    std::cout << "Спасибо за использование!\n";
    system("color 0B");
}

void ShowMainMenu() {
    // Показываем глафное меню.
    std::cout << "[1] Ввод графа\n"
                 "[2] Вывод графа\n"
                 "[3] Смена типа представления/хранения\n"
                 "[4] Подсчет степеней/полустепеней вершин\n"
                 "[5] Подсчет ребер/дуг\n"
                 "[6] Обход графа в глубину (стандартно)\n"
                 "[7] Обход графа в глубину (рекурсивно)\n"
                 "[8] Обход графа в ширину (стандартно)\n"
                 "[9] Обход графа в ширину (рекурсивно)\n"
                 "[10] Выйти из программы\n"
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
    // Если граф не свуществует, не будем выводить о нём никакой информации.
    if (!graph.Exist) {
        std::cout << "> Граф не существует, воспользуйтесь вводом.\n";
    } else  {
        // Печатаем основную информацию о графе.
        PrintGraphMainInfo(graph);
        std::cout << "Представление:\n";
        // Печатаем представление графа и информацию о его вершинах.
        PrintMatrixRepresentation(graph);
        PrintVertexDegrees(graph);
    }
    std::cout << "─────────────────────────────────────────────\n";
}

void PrintGraphMainInfo(const Graph &graph) {
    // Выводим предупреждение если граф не подходит под станартный функционал.
    if (graph.Pseudo)
        std::cout << "> [ОБНАРУЖЕН ПСЕВДОГРАФ] функционал ограничен.\n> [корректная работа не "
                "гарантируется]\n";
    else if (graph.Multi) {
        std::cout << "> [ОБНАРУЖЕН МУЛЬТИГРАФ] функционал ограничен.\n> [корректная работа не "
                "гаранитируется] \n";
    }
    // Выводим основные характеристики графа.
    std::cout << "> Тип представления графа: " << graph.type << ".\n";
    std::cout << "> Тип графа: " << (graph.Oriented ? "Ориентированный" : "Неориентированный")
              << ".\n";
    std::cout << "> Петли: " << (graph.WithLoops ? "Есть петли [корректная работа не "
                                              "гаранитруется]" : "Нет петель") << ".\n";
    if  (graph.WithLoops) {
        std::cout << "> Количество петель: " << graph.loops << "\n";
    }
    std::cout << "> Вершины: " << graph.p << ".\n";
    // В зависимости от типа графа вывоим количество дуг или ребер.
    if (!graph.Oriented) {
        std::cout << "> " << "Рёбра: " << graph.q << ".\n";
    } else {
        std::cout << "> " << "Дуги: " << graph.arcs << ".\n";
    }
}

void PrintMatrixRepresentation(const Graph &graph) {
    // В зависимости от типа хранения графа, выводим его представление на экран.
    if (graph.type == "Матрица смежности") {
        std::string output = GetStringAdjacencyMatrix(graph);
        std::cout << output;
    } else if (graph.type == "Матрица инцидентности") {
        std::string output = GetStringIncidenceMatrix(graph);
        std::cout << output;
    } else if (graph.type == "Список смежности") {
        std::string output = GetStringAdjacencyList(graph);
        std::cout << output;
    } else if (graph.type == "Список ребер") {
        std::string output = GetStringRibsList(graph);
        std::cout << output;
    } else if (graph.type == "FO") {
        std::string output = GetStringFO(graph);
        std::cout << output;
    } else if (graph.type == "FI") {
        std::string output = GetStringFI(graph);
        std::cout << output;
    } else if (graph.type == "MFO") {
        std::string output = GetStringMFO(graph);
        std::cout << output;
    } else if (graph.type == "MFI") {
        std::string output = GetStringMFI(graph);
        std::cout << output;
    } else if (graph.type == "BFO") {
        std::string output = GetStringBFO(graph);
        std::cout << output;
    } else if (graph.type == "BMFO") {
        std::string output = GetStringBMFO(graph);
        std::cout << output;
    }
}

void PrintVertexDegrees(const Graph &graph) {
    // Если граф ориентрованный - выводим информацию о степенях вершины как о полустепенях.
    // Иначе выводим степени вершин.
    if (graph.Oriented) {
        std::string output;
        output += "Полустепени вершин:\n";
        output += " \tИсход\tЗаход\n";
        for (std::size_t i = 0; i < graph.vertex_degrees.size(); ++i) {
            output += std::__cxx11::to_string(i + 1) + ": ";
            for (int j : graph.vertex_degrees[i]) {
                output += "\t" + std::__cxx11::to_string(j);
            }
            output += "\n" ;
        }
        std::cout << output;
    } else {
        std::string output;
        output += "Cтепени вершин:\n";
        for (std::size_t i = 0; i < graph.vertex_degrees.size(); ++i) {
            output += std::__cxx11::to_string(i + 1) + ": ";
            for (int j : graph.vertex_degrees[i]) {
                output += std::__cxx11::to_string(j) + "";
            }
            output += "\n" ;
        }
        std::cout << output;
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
            BFS(graph);
            break;
        case 10:
            std::cout << ">>>>>>>>>>>>>>>>>>>>>\nВыходим из программы!\n";
            break;
        default:
            break;
    }
}

void GetNewGraph(Graph& graph) {
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к вводу графа!";
    std::cout << "\n─────────────────────────────────────────────\n";
    cout << "[1] Через консоль\n[2] Через файл Input.txt (В одной директории с CmakeList.txt)\n";
    std::cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(2);
    std::cout << "─────────────────────────────────────────────\n";
    // По выбору пользователя приступаем к получению графа через определенный тип ввода.
    if (choice == 1) {
        GetConsoleGraphInput(graph);
    } else {
        GetFileGraphInput(graph);
    }
}

void GetFileGraphInput(Graph &graph) {
    std::cout << "─────────────────────────────────────────────\nВвод будет производится через"
                 " файл.\nВыберем тип ввода!\n─────────────────────────────────────────────\n";
    std::cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n[4]"
            " Список ребер\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n[9] BMFO\n[10] BFO\n";
    std::cout << "─────────────────────────────────────────────\n";
    switch (GetChoiceVarious(10)) { // Получаем от пользователя его выбор и запускаем.
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
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<int> bfo_fo{};
    std::string type = "BFO";
    int32_t number_of_strings = 1;
    std::cout << "Введите размер массива BFO:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива BFO");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        std::vector<std::string> string_data = ReadAllLinesInFile();  // Cчитываем весь файл.
        if (string_data.size() != number_of_strings) {
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Разделяем строку по пробелам.
            if (str.size() != number_of_colums) {
                std::cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, 0, 7)) {  // Проверяем строку на валидность.
                bfo_fo = final;
                final = {};
            } else {
                std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    adjacency_matrix = ParseFromBFO(bfo_fo);  // Получаем матрицу смежности из представления.
    if (adjacency_matrix.empty()) {  // Если прошлое действие завершилось ошибкой.
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetBMFOFromFile(Graph &graph) {
    // Обьявляем переменные для хранения представления.
    std::vector<int> bmfo_me{};
    std::vector<int> bmfo_mv{};
    // Получаем всю необходимую информацию для фиксации представления.
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    cout << "─────────────────────────────────────────────\n"
            "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Проверяем и получаем всю информацию.
    if (!TryGetInfoBMFOFromFile(number_of_colums, bmfo_me, bmfo_mv)) {
        return;
    }
    // Получаем матрицу смежности из представления.
    vector<vector<int>> adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем полученный граф.
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, "BMFO");
}

bool TryGetInfoBMFOFromFile(int32_t number_of_colums, vector<int> &bmfo_me, vector<int> &bmfo_mv) {
    try {
        // Считываем всю информацио из файла и проверяем построчную размерность.
        std::vector<std::string> string_data = ReadAllLinesInFile();
        if (string_data.size() != 2) {
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        // Получаем значение MV-вектора из файла с проверкой размерности самой строки-представления.
        std::vector<int> final;
        std::vector<std::string> str = Split(string_data[0], ' ');
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) bmfo_mv = final;
        else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        final = {};
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        // Получаем значение ME-вектора из файла с проверкой размерности самой строки-представления.
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) bmfo_me = final;
        else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetMFIFromFile(Graph &graph) {
    // Обьявляем переменные для хранения представления.
    std::vector<int> mfi_me{};
    std::vector<int> mfi_mv{};
    // Устанавливаем тип представления.
    std::string type = "MFI";
    // Получаем всю необходимую информацию для фиксации представления.
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Проверяем и получаем всю информацию.
    if (!TryGetMFIFromFile(number_of_colums, mfi_me, mfi_mv)) {
        return;
    }
    // Получаем матрицу смежности из данного представления.
    vector<vector<int>> adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем всю информацию о графе в структуру.
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

bool TryGetMFIFromFile(int32_t number_of_colums, vector<int> &mfi_me, vector<int> &mfi_mv) {
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем все строки из файла.
        if (string_data.size() != 2) {
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        std::vector<int> final;
        std::vector<std::string> str = Split(string_data[0], ' ');  // Делим строку по побелам.
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) {  // Проверяем на валидность.
            mfi_mv = final;
            final = {};
        } else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) { // Снова проверяем на валидность.
            mfi_me = final;
            final = {};
        } else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetMFOFromFile(Graph &graph) {
    // Обьявляем переменные для хранения представления.
    std::vector<int> mfo_me{};
    std::vector<int> mfo_mv{};
    // Устанавливаем тип представления.
    std::string type = "MFO";
    // Получаем всю необходимую информацию для фиксации представления.
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Проверяем и получаем всю информацию.
    if (!TryGetMFOFromFile(number_of_colums, mfo_me, mfo_mv)) {
        return;
    }
    // Получаем матрицу смежности из данного представления.
    vector<vector<int>> adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем всю информацию о графе в структуру.
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

bool TryGetMFOFromFile(int32_t number_of_colums, vector<int> &mfo_me, vector<int> &mfo_mv) {
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем всю информацию из файла.
        if (string_data.size() != 2) {
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return false;
        }
        std::vector<int> final;
        std::vector<std::string> str = Split(string_data[0], ' ');  // Делим строку по пробелам.
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) {  // Проверяем валидность строки-вектора.
            mfo_mv = final;
            final = {};
        } else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        str = Split(string_data[1], ' ');
        if (str.size() != number_of_colums) {
            std::cerr << "Размер строки не соответствует измерениям!\nНовый граф не установлен!\n";
            return false;
        }
        if (IsValidForMatrix(str, final, 0, 7)) { // Проверяем валидность строки-вектора.
            mfo_me = final;
            final = {};
        } else {
            std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
            return false;
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return false;
    }
    return true;
}

void GetFIFromFile(Graph &graph) {
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<int> fi{};  // Обьявляем переменные для хранения представления.
    std::string type = "FI";  // Устанавливаем тип представления.
    int32_t number_of_strings = 1;
    std::cout << "Введите размер массива FI:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива FI");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем всю информацию из файла.
        if (string_data.size() != number_of_strings) {  // Проверяем размерность представления.
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {  // Проходим по каждой строке представления.
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Делим строки по пробелам.
            if (str.size() != number_of_colums) {
                std::cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, 0, 7)) {  // Проверяем строку представления.
                fi = final;
                final = {};
            } else {
                std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    adjacency_matrix = ParseFromFI(fi);  // Получаем матрицу смежности из данного представления.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetFOFromFile(Graph &graph) {
    std::vector<int> fo{};
    std::string type = "FO";
    int32_t  number_of_strings = 1;
    std::cout << "Введите размер массива FO:\n";
    int32_t number_of_colums = GetChoiceVarious(100, "размер массива FO");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем всю информацию из файла.
        if (string_data.size() != number_of_strings) {  // Проверяем размерность представления.
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {  // Проходим по каждой строке представления.
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Делим строки по пробелам.
            if (str.size() != number_of_colums) {
                std::cerr << "Размер одной из строк не соответствует измерениям!"
                        "\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, 0, 7)) {  // Проверяем строку представления.
                fo = final;
                final = {};
            } else {
                std::cerr
                        << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    std::vector<std::vector<int>>  adjacency_matrix = ParseFromFO(fo);
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetRibsListFromFile(Graph &graph) {
    std::vector<std::vector<int>> ribs_list{};
    std::string type = "Список ребер";
    std::cout << "Введите количество дуг/ребер для списка ребер:\n";
    int32_t  number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем всю информацию из файла.
        if (string_data.size() != number_of_strings) {  // Проверяем размерность представления.
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {  // Проходим по каждой строке представления.
            int32_t number_of_colums = 2;
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Делим строки по пробелам.
            if (str.size() != number_of_colums) {
                cerr << "Размер одной из строк не соответствует измерениям!\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, 1, 7)) {  // Проверяем строку представления.
                ribs_list.push_back(final);
                final = {};
            } else {
                std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromRibsList(ribs_list, number_of_strings);
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    CheckAndSetFileData(graph, static_cast<int>(adjacency_matrix.size()), adjacency_matrix, type);
}

void GetAdjacencyListFromFile(Graph &graph) {
    std::vector<std::vector<int>> adjacency_list{};
    std::string type = "Список смежности";
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        vector<string> string_data = ReadAllLinesInFile();  // Считываем всю информацию из файла.
        if (string_data.size() != number_of_strings) {  // Проверяем размерность представления.
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        int counter = 0;
        for (auto &i: string_data) {  // Проходим по каждой строке представления.
            std::cout << "Введите количество смежных вершин для вершины." << ++counter << "\n";
            int32_t number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Делим строки по пробелам.
            if (str.size() != number_of_colums) {
                cerr << "Размер строк не соответствует измерениям!\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, 1, 7)) {  // Проверяем строку представления.
                adjacency_list.push_back(final);
                final = {};
            } else {
                std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    vector<vector<int>> adjacency_matrix = ParseFromAdjacencyList(adjacency_list, number_of_strings);
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    CheckAndSetFileData(graph, number_of_strings, adjacency_matrix, type);
}

void GetIncidenceMatrixFromFile(Graph &graph) {
    std::vector<std::vector<int>> incidence_matrix{};
    std::string type = "Матрица инцидентности";
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество ребер:\n";
    int32_t number_of_colums = GetChoiceVarious(50, "количество рёбер");
    std::cout << "─────────────────────────────────────────────\n";
    try {
        std::vector<std::string> string_data = ReadAllLinesInFile();  // Считываем информацию.
        if (string_data.size() != number_of_strings) {  // Проверяем размерность представления.
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto &i: string_data) {  // Проходим по каждой строке представления.
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');  // Делим строки по пробелам.
            if (str.size() != number_of_colums) {
                cerr << "Размер строки не соответствует измерениям.\nНовый граф не установлен!\n";
                return;
            }
            if (IsValidForMatrix(str, final, -1, 1)) {  // Проверяем строку представления.
                incidence_matrix.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
    } catch (std::exception &) {
        std::cerr << "Произошла ошибка при считывании файла!";
        return;
    }
    auto adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
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
    switch (GetChoiceVarious(10)) {  // По выбору пользователя получаем значение и выполняем опцию.
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
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    // Обьявляем переменные для хранения представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<int> bfo_fo{};
    // Утверждаем тип представления.
    std::string type = "BFO";
    number_of_strings = 1;
    std::cout << "Введите размер массива BFO:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива BFO");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                            "массива BFO");
        bfo_fo = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromBFO(bfo_fo);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetBMFOFromConsole(Graph &graph) {
    int32_t number_of_colums = 0;
    std::vector<std::vector<int>> adjacency_matrix{};
    // Обьявляем переменные для хранения представления.
    std::vector<int> bmfo_me{};
    std::vector<int> bmfo_mv{};
    // Утверждаем тип представления.
    std::string type = "BMFO";
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество элементов массива MV:\n";
    number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 100,
                                                            "массива MV");
        bmfo_mv = matrix_line;
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                           "массива ME");
        bmfo_me = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetMFIFromConsole(Graph &graph) {
    std::vector<std::vector<int>> adjacency_matrix{};
    // Обьявляем переменные для хранения представления.
    std::vector<int> mfi_me{};
    std::vector<int> mfi_mv{};
    // Утверждаем тип представления.
    std::string type = "MFI";
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 100, "массива MV");
        mfi_mv = matrix_line;
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7, "массива ME");
        mfi_me = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetMFOFromConsole(Graph &graph) {
    int32_t size_of_matrix = 0;
    std::vector<std::vector<int>> adjacency_matrix{};
    // Обьявляем переменные для хранения представления.
    std::vector<int> mfo_me{};
    std::vector<int> mfo_mv{};
    // Утверждаем тип представления.
    std::string type = "MFO";
    std::cout << "Введите количество вершин:\n";
    int32_t number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество элементов массива MV:\n";
    int32_t number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 100, "массива MV");
        mfo_mv = matrix_line;
        std::cout << "Введите количество элементов массива ME:\n";
        number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
        std::cout << "─────────────────────────────────────────────\n";
        matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7, "массива ME");
        mfo_me = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetFIFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    std::vector<std::vector<int>> adjacency_matrix{};
    // Обьявляем переменные для хранения представления.
    std::vector<int> fi{};
    // Утверждаем тип представления.
    type = "FI";
    number_of_strings = 1;
    std::cout << "Введите размер массива FI:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива FI");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                            "массива FI");
        fi = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromFI(fi);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetFOFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    std::vector<std::vector<int>> adjacency_matrix{};
    // Обьявляем переменные для хранения представления.
    std::vector<int> fo{};
    // Утверждаем тип представления.
    type = "FO";
    number_of_strings = 1;
    std::cout << "Введите размер массива FO:\n";
    number_of_colums = GetChoiceVarious(100, "размер массива FO");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                            "массива FO");
        fo = matrix_line;
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromFO(fo);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetRibsListFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    // Обьявляем переменные для хранения представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<std::vector<int>> ribs_list{};
    std::cout << "Введите количество вершин:\n";
    number_of_colums = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    // Утверждаем тип представления.
    type = "Список ребер";
    std::cout << "Введите количество дуг/ребер для списка ребер:\n";
    number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(2, 1, 7,
                                                            "списка ребер");
        ribs_list.push_back(matrix_line);
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromRibsList(ribs_list, number_of_colums);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyListFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    // Обьявляем переменные для хранения представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<std::vector<int>> adjacency_list{};
    // Утверждаем тип представления.
    type = "Список смежности";
    std::cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        std::cout << "Введите количество смежных вершин для вершины " << i + 1 << "\n";
        number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                            "списка смежности");
        adjacency_list.push_back(matrix_line);
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromAdjacencyList(adjacency_list, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetIncidenceMatrixFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    // Обьявляем переменные для хранения представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    std::vector<std::vector<int>> incidence_matrix{};
    // Утверждаем тип представления.
    type = "Матрица инцидентности";
    std::cout << "Введите количество вершин:\n";
    number_of_strings = GetChoiceVarious(7, "количество вершин");
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Введите количество ребер:\n";
    number_of_colums = GetChoiceVarious(50, "количество рёбер");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < number_of_strings; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, -1, 1,
                                                            "матрицы инцидентности");
        incidence_matrix.push_back(matrix_line);
    }
    // Получаем матрицу смежности из данного представления.
    adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyMatrixFromConsole(Graph &graph) {
    std::string type;
    int32_t size_of_matrix = 0;
    int32_t number_of_strings = 0;
    int32_t number_of_colums = 0;
    // Обьявляем переменные для хранения представления.
    std::vector<std::vector<int>> adjacency_matrix{};
    // Утверждаем тип представления.
    type = "Матрица смежности";
    std::cout << "Введите размер квадртаной матрицы смежности:\n";
    size_of_matrix = GetChoiceVarious(7, "размер матрицы");
    std::cout << "─────────────────────────────────────────────\n";
    // Получаем основные строки представления в соответсвии с настройками пользователя.
    for (int i = 0; i < size_of_matrix; ++i) {
        // Подвергаем каждое строковое представление проверке.
        std::vector<int> matrix_line = GetMatrixLine(size_of_matrix);
        adjacency_matrix.push_back(matrix_line);
    }
    // Останавливаем при наличии ошибки.
    if (adjacency_matrix.empty()) {
        std::cerr << "Было введено недопустимое сочетание данных для представления!\n";
        return;
    }
    // Устанавливаем данное представление в качетсве представления для нашего графа.
    SetAdjacencyMatrix(graph, adjacency_matrix, type);
}

void GetAdjacencyMatrixFromFile(Graph &graph) {
    // Получаем матрицу смежности из файла с помощью построковой проверки входных данных.
    std::string type = "Матрица смежности";
    std::cout << "Введите размер квадртаной матрицы смежности в файле (должна точно совпадать):\n";
    int32_t size_of_matrix = GetChoiceVarious(7, "размер матрицы");
    std::cout << "─────────────────────────────────────────────\n";
    std::vector<std::vector<int>> adjacency_matrix;
    try {
        std::vector<std::string> string_data = ReadAllLinesInFile();
        if (string_data.size() != size_of_matrix) {
            std::cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
            return;
        }
        for (auto & i : string_data) {
            std::vector<int> final;
            std::vector<std::string> str = Split(i, ' ');
            // После разделения по пробелам проверяем размерность представления.
            if (str.size() != size_of_matrix) {
                std::cerr << "Размер одной из строк не соответствует измерениям квадратной матрицы"
                        ".\nНовый граф не установлен!\n";
                return;
            }
            // Проверяем валидна ли строка из файла для матрицы смежности.
            if (IsValidForMatrix(str, final)) {
                adjacency_matrix.push_back(final);
                final = {};
            } else {
                std::cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
        // Устанавливаем информацию о графе.
        CheckAndSetFileData(graph, size_of_matrix, adjacency_matrix, type);
    } catch (std::exception&) {
        std::cerr << "Произошла неизвестная ошибка при чтении из файла!\n";
        return;
    }
}

void CheckAndSetFileData(Graph &graph, int32_t size_of_matrix,
                         const vector<vector<int>> &adjacency_matrix, string type) {
    // Производим проверку матрицы смежности, чтобы она точно была подходящей для построения графа.
    if (!adjacency_matrix.empty() && adjacency_matrix.size() == size_of_matrix) {
        // Заполняем структуру графа.
        SetAdjacencyMatrix(graph, adjacency_matrix, std::move(type));
        std::cout << "Успех! Матрица установлена!\n";
    } else if (adjacency_matrix.empty()) {
        std::cerr << "Не удалось считать матрицу, она пуста или невалидна!\nНовый граф не "
                "установлен.\n";
    } else {
        std::cerr << "Ошибка размерности матрицы или она не подходит под формат!\nНовый граф не "
                "установлен.\n";
    }
}

std::vector<std::string> ReadAllLinesInFile() {
    // Cчитываем данные из файла.
    std::string line = ReadFileIntoString("..\\Input.txt");
    // Делим на строки по переводу строки.
    std::vector<std::string> string_data = SplitSegment(line);
    // Выводим содержимое файла на экран.
    std::cout << "<< Начало содержимого файла >>\n";
    int counter_strings = 1;
    for (auto & i : string_data) {
        std::cout << counter_strings++ << "|\t" << i << std::endl;
    }
    std::cout << "<< Конец содержимого файла >>\n";
    // Возвращаем информацию.
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
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    // Справшиваем у пользователя предпочтительный стиль вывода.
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к выводу графа в текущем представлении";
    std::cout << "\n─────────────────────────────────────────────\n";
    std::cout << "[1] В консоль\n[2] В файл Output.txt (В одной директории с CmakeList.txt)\n";
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
                // Получаем строковое представления текущего графа.
                std::string output = GetStringMatrixRepresentation(graph);
                const char * str = output.c_str();
                // Производим запись.
                bool result = fputs(str, file);
                if (!result) std::cout << "Запись прошла успешно!\n";
            }
            else {
                std::cerr << "Нет доступа к файлу!\n";
            }
            fclose(file);
        } catch (std::exception& e) {
            std::cerr << "Произошла ошибка при записи в файл!\n";
        }
    }
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraph(Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к смене типа представления/хранения графа";
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
    std::cout << "Для ориентированного графа доступны следующие представления.\n";
    std::cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n"
            "[4] Список ребёр\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // По выбору пользователя меняем тип представления графа.
    switch (GetChoiceVarious(8)) {
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
    std::cout << "Успех!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForNotOriented(Graph &graph) {
    std::cout << "Для неориентированного графа доступны следующие представления.\n";
    std::cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n"
            "[4] Список ребёр\n[5] FO\n[6] MFO\n[7] BFO\n[8] BMFO\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // По выбору пользователя меняем тип представления графа.
    switch (GetChoiceVarious(8)) {
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
    std::cout << "Успех!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForMulti(Graph &graph) {
    std::cout << "Для неориентированного мультиграфа доступны следующие представления.\n";
    std::cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n"
            "[4] FO\n[5] MFO\n[6] BFO\n[7] BMFO\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // По выбору пользователя меняем тип представления графа.
    switch (GetChoiceVarious(8)) {
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
    std::cout << "Успех!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForPseudoNotOriented(Graph &graph) {
    std::cout << "Для неориентированного псеводографа доступны следующие представления.\n";
    std::cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n[4] FO\n[5] MFO\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // По выбору пользователя меняем тип представления графа.
    switch (GetChoiceVarious(5)) {
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
    std::cout << "Успех!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void ChangeTypeOfGraphForPseudoOriented(Graph &graph) {
    std::cout << "Для ориентированного псеводографа доступны следующие представления.\n";
    std::cout << "[1] Матрица смежности\n[2] Список смежности\n[3] Список ребёр\n"
            "[4] FO\n[5] FI\n[6] MFO\n";
    std::cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(6);
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // По выбору пользователя меняем тип представления графа.
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
    std::cout << "Успех!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void CountDegreesOfVertices(const Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к подсчёту степеней/полустепеней графа\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // Печатаем информацию о вершинах графа.
    PrintVertexDegrees(graph);
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void CountEdgesOfGraph(const Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к подсчёту рёбер/дуг графа\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    // В зависимости от типа графа возвращаем пользователю информацию о ребрах или о дугах.
    if (!graph.Oriented) {
        std::cout << "" << "Рёбра: " << graph.q << ".\n";
    } else {
        std::cout << "" << "Дуги: " << graph.arcs << ".\n";
    }
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void DFSClassic(const Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к обходу графа в глубину стандартно!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    std::stack<int> stack;
    std::vector<std::vector<int>> adjacency_matrix = graph.adjacency_matrix;  // Матрица смежности.
    std::vector<int> used(adjacency_matrix.size());  // Место для информации о вершинах графа.
    std::vector<int> path;  // Путь по вершинам.
    for (int & i : used) i = 0;
    stack.push(0);
    // Пока стэк не пустой, продолжаем работу.
    while (!stack.empty())
    {
        int now_point = stack.top();  // Получаем вершину.
        stack.pop();
        // Eсли вершина посещена, то пропускаем итерацию
        if (used[now_point] == 2) continue;
        if (path.empty()) std::cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else std::cout << "Путь: " << path[path.size() - 1] << " --> " << now_point + 1 << "\n";
        std::cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = static_cast<int>(adjacency_matrix.size())-1; j >= 0; j--)
        {
            if (adjacency_matrix[now_point][j] == 1 && used[j] != 2)
            {
                std::cout << "Обнаружили смежную вершину " << j + 1 << "\n";
                stack.push(j);
                used[j] = 1;
                path.push_back(now_point + 1);
            }
        }
        std::cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
    }
    std::cout << "Обход в глубину стандартным методом завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void DFSSearchRecursion(int32_t start, int32_t end, const std::vector<std::vector<int>>& matrix,
                        std::vector<int>& used, std::vector<int>& path)
{
    if (path.empty()) {
        std::cout << "Cтартовая точка: " << start + 1 << "\n";
    }
    std::cout << "Посетили вершину " << start + 1 << "\n";
    path.push_back(start + 1);
    std::cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n";
    used[start] = 1;
    for (int i = 0; i < end; i++)
        // Если вершина еше не посешена, идем в неё
        if (matrix[start][i] != 0 && used[i] == 0) DFSSearchRecursion(i, end, matrix, used, path);
}

void DFSRecursion(const Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к обходу графа в глубину рекурсивно!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    std::vector<int> used(graph.adjacency_matrix.size());
    std::vector<int> path;
    for (int & i : used) i = 0;
    // Запускаем рекурсивный проход по врешинам.
    DFSSearchRecursion(0, static_cast<int>(graph.adjacency_matrix.size()), graph.adjacency_matrix,
                       used, path);
    std::cout << "Обход в глубину рекурсивным методом завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void BFS(const Graph& graph) {
    if (!graph.Exist) {
        std::cerr << "Граф не существует. Воспользуйтесь вводом!\n";
        return;
    }
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "Приступаем к обходу графа в ширину!\n";
    std::cout << "─────────────────────────────────────────────\n";
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    std::queue<int> queue;
    std::vector<std::vector<int>> adjacency_matrix = graph.adjacency_matrix;
    std::vector<int> used(adjacency_matrix.size()); // Вектор для ифнормации по вершинам графа.
    std::vector<int> path;
    for (int & i : used) i = 0;
    queue.push(0);
    while (!queue.empty())
    {
        int now_point = queue.front(); // Получаем вершину.
        queue.pop();
        if (used[now_point] == 2) continue;
        if (path.empty()) std::cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else std::cout << "Путь: " << path[path.size() - 1] << " --> " << now_point + 1 << "\n";
        std::cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = 0; j < adjacency_matrix.size(); j++)
        {
            // Если вершина не посещена, то выполняем обнаружение.
            if (adjacency_matrix[now_point][j] == 1 && used[j] != 2)
            {
                std::cout << "Обнаружили смежную вершину " << j + 1 << "\n";
                queue.push(j);
                used[j] = 1;
                path.push_back(now_point + 1);
            }
        }
        std::cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n"; // выводим номер вершины
    }
    std::cout << "Обход в ширину завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

bool IsOriented(std::vector<std::vector<int>> matrix) {
    // Если окажется, что одно из ребер не введет из одной вершину в другую и обратно, то граф
    // ориентированный.
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix[i].size(); ++j) {
            if (i != j && matrix[i][j] != matrix[j][i]) {
                return true;
            }
        }
    }
    return false;
}

bool IsMulti(const std::vector<std::vector<int>> &matrix) {
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

bool IsPseudo(const std::vector<std::vector<int>>& matrix, bool oriented) {
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

int CountLoops(std::vector<std::vector<int>> matrix) {
    // Считаем петли проходясь по ребрам, которые ведут из вершины в сами себя.
    int count = 0;
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix[i].size(); ++j) {
            if (i == j) {
                if (matrix[i][j] > 0) {
                    count += matrix[i][j];
                }
            }
        }
    }
    return count;
}

int CountArcs(const std::vector<std::vector<int>>& matrix) {
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

int CountRibs(std::vector<std::vector<int>> matrix) {
    // Считаем все ребра с учетом петлей и ребер;
    int count = 0;
    int anti_count = 0;
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix[i].size(); ++j) {
            if (matrix[i][j] > 0 && matrix[j][i] == matrix[i][j] && i != j) {
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
    // Получаем представление о степенях вершин с учетом характеристик графа. Для графа с петлями
    // ведем отдельный подсчет, ведь степень такой вершины будет больше на 1 обычной.
    std::vector<std::vector<int>> vertex_degrees(matrix.size());
    if (!with_loops && !oriented) {
        for (std::size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
    } else if (with_loops && !oriented) {
        for (std::size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            int32_t loops = 0;
            for (std::size_t j = 0; j < matrix[i].size(); ++j) {
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
        for (std::size_t i = 0; i < matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
        std::vector<std::vector<int>> new_matrix(matrix.size());
        for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                        .push_back(j[i]);
        for (std::size_t i = 0; i < new_matrix.size(); ++i) {
            int32_t sum = 0;
            for (int j : new_matrix[i]) sum += j;
            vertex_degrees[i].push_back(sum);
        }
    }
    return vertex_degrees;
}

vector<vector<int>> ParseInIncidenceMatrix(const vector<vector<int>>& adjacency_list) {
    std::vector<std::vector<int>> incidence;
    // Обявляем переменную множетво, чтобы фииксировать.
    std::set<std::pair<int, int>> rejected;
    // Без повторений фиксируем смежность вершин в матрице инцинденций.
    for (std::size_t  i = 0; i < adjacency_list.size(); ++i) {
        for (std::size_t  j = 0; j < adjacency_list[i].size(); ++j) {
            if (!rejected.contains({i, adjacency_list[i][j]-1})) {
                std::vector<int> work_flow(adjacency_list.size());
                auto pointer = std::find(adjacency_list[adjacency_list[i][j] - 1].begin(),
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
                // Заносим запрещенные координаты.
                rejected.insert({adjacency_list[i][j]-1, i});
                incidence.push_back(work_flow);
            }
        }
    }
    // Транспонируем конечную матрицу.
    std::vector<std::vector<int>> new_matrix(adjacency_list.size());
    for (std::size_t i = 0; i < adjacency_list.size(); ++i){
        for (auto & j : incidence) {
            new_matrix[i].push_back(j[i]);
        }
    }
    return new_matrix;
}

std::vector<std::vector<int>> ParseInAdjacencyList(const std::vector<std::vector<int>>& matrix) {
    // Фиксируем в виде векторов смежные вершины, проходясь по матрице по строкам.
    std::vector<std::vector<int>> adjacency_list(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
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
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
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

std::vector<int> ParseInFO(const std::vector<std::vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, смежные с текущей вершиной, деля их 0.
    std::vector<int> fo;
    // Выставляем количество вершин.
    fo.push_back(static_cast<int>(matrix.size()));
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    fo.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
            }
        }
        // Выставляем 0 как разделитель.
        fo.push_back(0);
    }
    return fo;
}

std::vector<int> ParseInFI(const std::vector<std::vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, входящие в текущую вершину, деля их
    // 0-разделителем.
    std::vector<int> fi;
    if (!oriented) {
        return {};
    }
    // Транспонируем входную матрицу.
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    // Выставляем колличество вершин.
    fi.push_back(static_cast<int>(new_matrix.size()));
    for (std::size_t i = 0; i < new_matrix.size(); ++i) {
        for (std::size_t j = 0; j < new_matrix.size(); ++j) {
            if (new_matrix[i][j] > 0) {
                int num_of_path = new_matrix[i][j];
                while (num_of_path != 0) {
                    fi.push_back(static_cast<int>(j) + 1);
                    --num_of_path;
                }
            }
        }
        // Выставляем 0 как разделитель.
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
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
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
    // Трансопнируем входную матрицу смежности.
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    int last = 0;
    for (std::size_t i = 0; i < new_matrix.size(); ++i) {
        for (std::size_t j = 0; j < new_matrix.size(); ++j) {
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

std::vector<int> ParseInBFO(const std::vector<std::vector<int>>& matrix, bool oriented) {
    // Фиксируем в виде одномерного вектора вершины, смежные с текущей вершиной, но с номером не
    // ниже текущей вершины, деля их 0.
    if (oriented) {
        return {};
    }
    std::vector<int> fo;
    // Устанавливаем первым делом количество вершин.
    fo.push_back(static_cast<int>(matrix.size()));
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
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
        // Устанавливаем 0 как разделитель.
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
    for (std::size_t i = 0; i < matrix.size(); ++i) {
        for (std::size_t j = 0; j < matrix.size(); ++j) {
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

std::string GetStringMatrixRepresentation(const Graph &graph) {
    // В зависимости от типа хранения возфращаем представление информации о графе в качестве строки.
    std::string output;
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

std::string GetStringBMFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.bmfo_me) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    output += "MV: [";
    check = 0;
    for (int i : graph.bmfo_mv) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + std::__cxx11::to_string(graph.p) + "\n";
    return output;
}

std::string GetStringBFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "BFO: [";
    int check = 0;
    for (int i : graph.bfo_fo) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n";
    return output;
}

std::string GetStringMFI(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.mfi_me) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    output += "MV: [";
    check = 0;
    for (int i : graph.mfi_mv) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + std::__cxx11::to_string(graph.p) + "\n";
    return output;
}

std::string GetStringMFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "ME: [";
    int check = 0;
    for (int i : graph.mfo_me) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    check = 0;
    output += "MV: [";
    for (int i : graph.mfo_mv) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\nP: " + std::__cxx11::to_string(graph.p) + "\n";
    return output;
}

std::string GetStringFI(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "FI: [";
    int check = 0;
    for (int i : graph.fi) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    return output;
}

std::string GetStringFO(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    output += "FO: [";
    int check = 0;
    for (int i : graph.fo) {
        output += std::__cxx11::to_string(i) + ", ";
        ++check;
    }
    if (check != 0) output = output.substr(0, output.size()-2);
    output += "]\n" ;
    return output;
}

std::string GetStringRibsList(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    std::string output;
    output += " \tНачало\tКонец\n";
    for (std::size_t i = 0; i < graph.ribs_list.size(); ++i) {
        output += std::__cxx11::to_string(i + 1) + ": ";
        for (int j : graph.ribs_list[i]) {
            output += "\t" + std::__cxx11::to_string(j);
        }
        output += "\n" ;
    }
    return output;
}

std::string GetStringAdjacencyList(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    std::string output;
    for (std::size_t i = 0; i < graph.adjacency_list.size(); ++i) {
        output += std::__cxx11::to_string(i + 1) + ": ";
        int check = 0;
        for (int j : graph.adjacency_list[i]) {
            output += std::__cxx11::to_string(j) + ",";
            ++check;
        }
        if (check != 0) output = output.substr(0, output.size()-1);
        output += "\n" ;
    }
    return output;
}

std::string GetStringIncidenceMatrix(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    if (graph.q == 0 || graph.arcs == 0) {
        return "Не обнаружено связей между вершинами! Переключитесь на матрицу смежности!\n";
    }
    std::string output = "[НУМЕРАЦИЯ РЁБЕР СОГЛАСНО МАТРИЦЕ СМЕЖНОСТИ]\n";
    for (int i = 0; i < graph.q; ++i) {
        output += "\t" + std::__cxx11::to_string(i + 1);
    }
    output += "\n";
    for (int i = 0; i < graph.p; ++i) {
        output += std::__cxx11::to_string(i + 1);
        for (int j = 0; j < graph.q; ++j) {
            output += "\t" + std::__cxx11::to_string(graph.incidence_matrix[i][j]);
        }
        output += "\n" ;
    }
    return output;
}

std::string GetStringAdjacencyMatrix(const Graph &graph) {
    // Создаём для пользователя наиболее понятное строковое представления информации о графе в
    // текущем представлении.
    std::string output;
    for (int i = 0; i < graph.p; ++i) {
        output += "\t" + std::__cxx11::to_string(i + 1);
    }
    output += "\n";
    for (int i = 0; i < graph.p; ++i) {
        output += std::__cxx11::to_string(i + 1);
        for (int j = 0; j < graph.p; ++j) {
            output +=  + "\t" + std::__cxx11::to_string(graph.adjacency_matrix[i][j]);
        }
        output += "\n" ;
    }
    return output;
}

int32_t GetChoice() {
    bool valid_choice = false;
    int32_t ans = 0;
    std::string line;
    std::cout << "Выбретите опцию, отправив число от 1 до 10.\n";
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
        if (ans >= 1 && ans <= 10) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до 10!\n";
        }
    } while (!valid_choice);
    system("color 0B");
    return ans;
}

int32_t GetChoiceVarious(int32_t upper_bound) {
    bool valid_choice = false;
    int32_t ans = 0;
    std::string line;
    std::cout << "Выбретите опцию, отправив число от 1 до " << upper_bound << ".\n";
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

int32_t GetChoiceVarious(int32_t upper_bound, const std::string& info) {
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

int32_t ParseNum(const std::string& string) {
    int32_t ans = 0;
    try {
        // Производим попытку преобразовать строку к числу.
        ans = stoi(string);
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

std::ifstream::pos_type GetFileSize(const std::string& filename)
{
    // Получаем длину файла в байтах, в симолах стандартной кодировки.
    std::ifstream in(filename, std::ifstream::ate | std::ifstream::binary);
    return in.tellg();
}

std::string ReadFileIntoString(const std::string& path) {
    std::ifstream input_file(path);
    try {
        int size = 0;
        // Получаем размер файла
        size = static_cast<int>(GetFileSize(path));
        // Провреяем размер файла.
        if (size > 30000) {
            std::cerr << "Отмена чтения. Файл весит больше 30 КБ!\n";
            return "";
        }
        // Получилось ли открыть файл.
        if (!input_file.is_open()) {
            std::cerr << "Невозможно открыть файл!\n";
            return "";
        }
        // Возвращаем всю строку из файла обратно.
        return {std::string((std::istreambuf_iterator<char>(input_file)),
                            std::istreambuf_iterator<char>())};
    } catch (std::exception&) {
        std::cerr << "Произошла ошибка при чтении из файла!\n";
        input_file.close();
    }
    return "";
}

std::vector<int> GetMatrixLine(int32_t size_of_matrix) {
    std::string line;
    bool flag = false;
    std::vector<int> final;
    std::vector<std::string> string_data;
    do {
        std::cout << "Введите строку матрицы размером " << size_of_matrix << std::endl;
        try {
            std::getline(std::cin, line);
            // Проверяем символ.
            int check = stoi(line);
            // Разделяем строку по пробелам.
            string_data = Split(line, ' ');
            // Проверяем размер строки.
            if (string_data.size() != size_of_matrix) {
                std::cout << "Размер не подходит!\n";
            } else {
                // Проверяем введеную строку на валидность.
                if (IsValidForMatrix(string_data, final)) {
                    std::cout << "Успешно получена строка матрицы!\n";
                    return final;
                }
            }
        } catch (std::exception&) {
            std::cout << "Введите подходящую строку!\n";
            // Возвращаем валидное значение строки.
            std::cin.clear();
            continue;
        }
    } while (!flag);
    return final;
}

std::vector<std::string> Split(const std::string& str, char delim) {
    std::vector<std::string> ans;
    try {
        std::stringstream str_stream(str);
        std::string item;
        // Получаем элементы через текущий разделитель и если это не пустые строки, заносим в
        // вектор.
        while (std::getline(str_stream, item, delim)) {
            if (item[0] != delim && !item.empty()) ans.push_back(item);
        }
        return ans;
    } catch (std::exception&) {
        std::cerr << "Не вводите пустую строку!\n";
        // При ошибке также очищаем консоль, возвращая ее в стандартное состояние.
        std::cin.clear();
    }
    return ans;
}

std::vector<std::string> SplitSegment(const std::string& sentence) {
    std::vector<std::string> ans;
    try {
        std::stringstream str_stream(sentence);
        std::string item;
        // Получаем элементы через перенос строки и если это не пустые строки, заносим в
        // вектор.
        while (std::getline(str_stream, item, '\n')) {
            if (item != " " && !item.empty()) ans.push_back(item);
        }
    } catch (std::exception&) {
        std::cerr << "Возникла ошибка при разделении файла на строки!\n";
    }
    return ans;
}

bool IsValidString(const std::string& str) {
    int32_t ans = 0;
    try {
        // Пробуем произвести приведение строки к числу.
        ans = stoi(str);
        // Проверяем диапазон.
        if (ans >= 0 && ans <= 4) {
            return true;
        } else {
            std::cerr << "Одно из чисел не в пределах от 0 до 4!\n";
            return false;
        }
    } catch (std::exception&){
        std::cerr << "Данные не содержат число!\n";
        return false;
    }
    return ans;
}

bool IsValidForMatrix(const std::vector<std::string>& line, std::vector<int>& ans){
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

bool IsValidString(const std::string& str, int32_t lower_bound, int32_t upper_bound) {
    int32_t ans = 0;
    try {
        // Пытаемся преобразовать строку к числу.
        ans = stoi(str);
        // Проверяем диапазон.
        if (ans >= lower_bound && ans <= upper_bound) {
            return true;
        } else {
            cerr << "Одно из чисел не в пределах от " << lower_bound << " до "
            << upper_bound << "!\n";
            return false;
        }
    } catch (std::exception& exception){
        std::cerr << "Данные не содержат число!\n";
        return false;
    }
    return ans;
}

int32_t ParseNum(const std::string& str, int32_t lower_bound, int32_t upper_bound) {
    int32_t ans = 0;
    try {
        // Производим попытку преобразовать строку к числу.
        ans = stoi(str);
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

std::vector<int> GetMatrixLineVarious(int32_t size_of_matrix, int32_t lower_bound,
                                      int32_t upper_bound, const std::string& comment) {
    std::string line;
    bool flag = false;
    std::vector<int> final;
    std::vector<std::string> string_data;
    do {
        std::cout << "Введите строку " << comment << " размером " << size_of_matrix << std::endl;
        try {
            // Получаем строку текущего представления.
            std::getline(std::cin, line);
            // Производим попытку преобразования первого символа в число.
            int check = stoi(line);
            // Делим строку по пробелам.
            string_data = Split(line, ' ');
            // Проверяем размер.
            if (string_data.size() != size_of_matrix) {
                std::cout << "Размер не подходит!\n";
            } else {
                // Если получаем подходящую строку - возвращаем.
                if (IsValidForMatrix(string_data, final, lower_bound, upper_bound)) {
                    std::cout << "Успешно получена строка " << comment << "!\n";
                    return final;
                }
            }
        } catch (std::exception& e) {
            std::cout << "Введите подходящую строку!\n";
            // При ошибке отчищаем консоль до последнего приемлемого состояния.
            std::cin.clear();
            continue;
        }
    } while (!flag);
    return final;
}

int Find(std::vector<int> &vector, int item) {
    // Ищем первое вхождение элемента в цикле.
    for (int i = 0; i < vector.size(); i++) {
        if (vector[i] == item) {
            return i;
        }
    }
    // Если не нашли - возвращаем -1.
    return -1;
}

std::vector<std::vector<int>> ParseFromAdjacencyList(std::vector<std::vector<int>> adjacency_list,
                                                     int32_t vertices) {
    // Проходимся по всем элементам списка смежности, заполняя по их индексу и самому значению
    // матрицу смежности. При этом фиксируем несуществующие вершины, игнорируя их.
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    std::vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    for (int i = 0; i < vertices; ++i) {
        for (int j = 0; j < vertices; ++j) {
            if (j < adjacency_list[i].size()) {
                //
                if (adjacency_list[i][j] > vertices) {
                    std::cout << "Предупреждение: обнаружена вершина " << adjacency_list[i][j] <<
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

std::vector<std::vector<int>> ParseFromIncidenceMatrix(vector<vector<int>> incidence_matrix,
                                                       int32_t vertices) {
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    std::vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;
    std::vector<std::vector<int>> matrix(incidence_matrix[0].size());
    // Транспонируем матрицу инциндентости.
    for (std::size_t i = 0; i < incidence_matrix[0].size(); i++)
        for (auto & j : incidence_matrix) matrix[i].push_back(j[i]);
    // Построчно изучаем каждое ребро в матрице.
    for (auto & i : matrix) {
        if (!GetStringOfAdjacencyMatrixFromIncidence(adjacency_matrix, i)){
            return {};
        }
    }
    return adjacency_matrix;
}

bool GetStringOfAdjacencyMatrixFromIncidence(vector<vector<int>> &adjacency_matrix, vector<int> &i) {
    // Находим информацию об смежности вершин с помощью поиска по элементам вектора.
    if  (find(i.begin(), i.end(), 1) != i.end()) {
        if (count(i.begin(), i.end(), 1) == 2 && count(i.begin(), i.end(), -1) == 0) {
            auto find_one = Find(i, 1);
            i[find_one] = 0;
            auto find_two = Find(i, 1);
            if (find_one != -1 && find_two != -1) {
                adjacency_matrix[find_one][find_two] = 1;
                adjacency_matrix[find_two][find_one] = 1;
            } else {
                std::cerr << "Неправильная конфигурация одного из ребер!\n";
                return false;
            }
        } else if ((count(i.begin(), i.end(), 1) == 1)) {
            if  ((count(i.begin(), i.end(), -1) == 1)) {
                auto find_one = Find(i, 1);
                auto find_two = Find(i, -1);
                if (find_two != -1 && find_one != -1) adjacency_matrix[find_one][find_two] = 1;
                else {
                    std::cerr << "Неправильная конфигурация одного из ребер!\n";
                    return false;
                }
            } else {
                std::cerr << "Возникла ошибка, в одном из ребер не нашлось входящей вершины "
                        "или их слишком много!\n";
                return false;
            }
        } else {
            std::cerr << "Возникла ошибка, в одном из ребер не нашлось выходящей вершины"
                    " или их слишком много!\n";
            return false;
        }
    } else {
        std::cerr << "Возникла ошибка, одно из ребер не может существовать!\n";
        return false;
    }
    return true;
}

vector<vector<int>> ParseFromRibsList(vector<vector<int>> ribs_list, int32_t vertices) {
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    std::vector<int> empty_vector(vertices);
    for (auto & i : adjacency_matrix) i = empty_vector;

    for (auto & i : ribs_list) {
        for (int j = 0; j < 1; ++j) {
            if (i[j] > vertices) {
                std::cout << "Предупреждение: обнаружена вершина " << i[j] <<
                     " в списке, которая не покрыта"
                     " количеством вершин, она проигнорирована!\n";
            } else if (i[j+1] > vertices) {
                std::cout << "Предупреждение: обнаружена вершина " << i[j + 1] <<
                     " в списке, которая не покрыта"
                     " количеством вершин, она проигнорирована!\n";
            } else  {
                adjacency_matrix[i[j]-1][i[j+1]-1] = 1;
            }
        }
    }

    return adjacency_matrix;
}

std::vector<std::vector<int>> ParseFromFO(std::vector<int> fo) {
    int counter = static_cast<int>(count(fo.begin(), fo.end(), 0));
    if (counter != fo[0]) {
        std::cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    std::vector<std::vector<int>> adjacency_matrix(counter);
    std::vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    std::vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fo.size(); ++i) {
        if (fo[i] == 0) {
            for (int j : vertex) {
                if (j >= counter) {
                    std::cerr << "Встречена вершина, которая не может находится в данном "
                                 "представлении!\n";
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

std::vector<std::vector<int>> ParseFromFI(std::vector<int> fi) {
    int counter = static_cast<int>(count(fi.begin(), fi.end(), 0));
    if (counter != fi[0]) {
        std::cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    std::vector<std::vector<int>> adjacency_matrix(counter);
    std::vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    std::vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fi.size(); ++i) {
        if (fi[i] == 0) {
            for (int j : vertex) {
                if (j >= counter) {
                    std::cerr << "Встречена вершина, которая не может находится в "
                                 "данном представлении!\n";
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
    std::vector<std::vector<int>> matrix(adjacency_matrix.size());
    for (std::size_t i = 0; i < adjacency_matrix.size(); i++)
        for (auto & j : adjacency_matrix) matrix[i].push_back(j[i]);
    return matrix;
}

vector<vector<int>> ParseFromMFO(const vector<int>& mv, vector<int> me, int32_t vertices) {
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    for (auto & i : adjacency_matrix) i = vector<int>(vertices);
    if  (mv.size() != vertices) {
        std::cerr << "Размерность MV должна совпадать с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    std::vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        if  (i == 0) continue;
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                std::cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Обнаружена вершина, которая не может быть в таком представлении\n";
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
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    for (auto & i : adjacency_matrix) i = vector<int>(vertices);
    if  (mv.size() != vertices) {
        cerr << "Размерность MV должна совпадать с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    std::vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                std::cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Обнаружена вершина, которая не может быть в таком представлении\n";
                        return {};
                    }
                    adjacency_matrix[count][k-1] = 1;
                }
                now_position = j+1;
                count += 1;
                vertex = {};
                break;
            } else
                vertex.push_back(me[j]);
        }
    }
    std::vector<std::vector<int>> ans(adjacency_matrix.size());
    for (size_t i = 0; i < adjacency_matrix.size(); i++)
        for (auto & j : adjacency_matrix) ans[i].push_back(j[i]);
    return ans;
}

vector<vector<int>> ParseFromBMFO(const vector<int>& mv, vector<int> me, int32_t vertices) {
    std::vector<std::vector<int>> adjacency_matrix(vertices);
    for (auto & i : adjacency_matrix) i = vector<int>(vertices);
    if  (mv.size() != vertices) {
        cerr << "Размерность MV должна совпадать с количеством вершин.\n";
        return {};
    }
    int now_position = 0;
    int count = 0;
    std::vector<int> vertex;
    for (int i : mv) {
        if  (i == 0) {
            count += 1;
            continue;
        }
        if  (i == 0) continue;
        for (int j = now_position; j < me.size(); ++j) {
            if (i > me.size()) {
                std::cerr << "MV массив указывает на несуществующий обьект в ME.\n";
                return {};
            }
            if (i == j+1) {
                vertex.push_back(me[j]);
                for (int k : vertex) {
                    if (k > vertices) {
                        cerr << "Обнаружена вершина, которая не может быть в таком представлении\n";
                        return {};
                    }
                    adjacency_matrix[count][k-1] = 1;
                    adjacency_matrix[k-1][count] = 1;
                }
                now_position = j+1;
                count += 1;
                vertex = {};
                break;
            } else
                vertex.push_back(me[j]);
        }
    }
    return adjacency_matrix;
}

std::vector<std::vector<int>> ParseFromBFO(std::vector<int> fo) {
    int counter = static_cast<int>(count(fo.begin(), fo.end(), 0));
    if (counter != fo[0]) {
        std::cerr << "Количество вершин не совпадает с конфигурацией массива!\n";
        return {};
    }
    std::vector<std::vector<int>> adjacency_matrix(counter);
    std::vector<int> empty_vector(counter);
    for (auto & i : adjacency_matrix) i = empty_vector;
    std::vector<int> vertex;
    int count = 0;
    for (int i = 1; i < fo.size(); ++i) {
        if (fo[i] == 0) {
            for (int j : vertex) {
                if (j >= counter) {
                    std::cerr << "Встречена вершина, которая не может находится"
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