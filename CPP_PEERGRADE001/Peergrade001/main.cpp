#include <iostream>  // Библиотека ввода-вывода.
#include <vector>
#include <set>
#include <limits>
#include <Windows.h>
#include "Functions.h" // Указываем заголовочный файл;
#include <cstdio>
#include <sstream>
#include <fstream>
#include <cmath>
#include <stack> // стек
#include <queue>

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

void GetAdjasmentMatrixFromFile(Graph &graph);
bool IsValidString(const string& s, int32_t lower_bound, int32_t upper_bound);
int32_t ParseNum(const string& s, int32_t lower_bound, int32_t upper_bound);
bool IsValidForMatrix(const vector<string>& line, vector<int>& ans, int32_t lower_bound,
                      int32_t upper_bound);
vector<int> GetMatrixLineVarious(int32_t size_of_matrix, int32_t lower_bound, int32_t upper_bound,
                          string comment);

vector<string> ReadAllLinesInFile();

void CheckAndSetFileData(Graph &graph, int32_t size_of_matrix,
                         const vector<vector<int>> &adjacency_matrix);

/**
 *  @brief  Основная функция работы программы.
 *  @return  0 при успешном выполнении, -1 при возникновении ошибки.
*/

int main() {
    SetConsoleOutputCP(CP_UTF8);
    const int32_t EXIT_NUM = 9;  // Переменная для проверки опции выхода из программы.
    int32_t now_choice;  // Текущий выбор меню пользователем.
    Graph graph = {false}; // Задаем граф.
    system("color 0B");
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
        PrintGraphMainInfo(graph);
        cout << "Представление:\n";
        PrintMatrixRepresentation(graph);
        PrintVertexDegrees(graph);
    }
    std::cout << "─────────────────────────────────────────────\n";
}

void PrintGraphMainInfo(const Graph &graph) {
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
            cout << "Выход\n";
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
        std::cout << "─────────────────────────────────────────────\n";
        cout << "Ввод будет производится через консоль.\n";
        cout << "Выберем тип ввода!";
        std::cout << "\n─────────────────────────────────────────────\n";
        cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n[4]"
                " Список ребер\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n[9] BMFO\n[10] BFO"
                "\n";
        std::cout << "─────────────────────────────────────────────\n";
        int32_t choice_second = GetChoiceVarious(10);

        vector<vector<int>> adjacency_matrix;
        vector<vector<int>> incidence_matrix{};
        vector<vector<int>> adjacency_list{};
        vector<vector<int>> ribs_list{};
        vector<int> fo{};
        // FI представление.
        vector<int> fi{};
        // MFO представление.
        vector<int> mfo_me{};
        vector<int> mfo_mv{};
        // MFI представление.
        vector<int> mfi_me{};
        vector<int> mfi_mv{};
        // BMFO представление.
        vector<int> bmfo_me{};
        vector<int> bmfo_mv{};
        // BFO представление.
        vector<int> bfo_fo{};

        int32_t size_of_matrix = 0;
        int32_t number_of_strings = 0;
        int32_t number_of_colums = 0;
        switch (choice_second) {
            case 1:
                cout << "Введите размер квадртаной матрицы смежности:\n";
                size_of_matrix = GetChoiceVarious(7, "размер матрицы");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < size_of_matrix; ++i) {
                    vector<int> matrix_line = GetMatrixLine(size_of_matrix);
                    adjacency_matrix.push_back(matrix_line);
                }
                break;
            case 2:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество ребер:\n";
                number_of_colums = GetChoiceVarious(50, "количество рёбер");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, -1, 1,
                                                                   "матрицы инцидентности");
                    incidence_matrix.push_back(matrix_line);
                }
                //adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
                break;
            case 3:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    cout << "Введите количество смежных вершин для вершины "<< i+1 <<"\n";
                    number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                                   "списка смежности");
                    adjacency_list.push_back(matrix_line);
                }
                //adjacency_matrix = ParseFromAdjacencyList(adjacency_list, number_of_strings);
                break;
            case 4:
                cout << "Введите количество дуг/ребер для списка ребер:\n";
                number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    number_of_colums = 2;
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                                   "списка ребер");
                    ribs_list.push_back(matrix_line);
                }
                //adjacency_matrix = ParseFromRibsList(ribs_list, number_of_strings);
                break;
            case 5:
                number_of_strings = 1;
                cout << "Введите размер массива FO:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива FO");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                                   "массива FO");
                    fo = matrix_line;
                }
                //adjacency_matrix = ParseFromFO(fo);
                break;
            case 6:
                number_of_strings = 1;
                cout << "Введите размер массива FI:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива FI");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                                   "массива FI");
                    fi = matrix_line;
                }
                //adjacency_matrix = ParseFromFI(fi);
                break;
            case 7:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                                   "массива MV");
                    mfo_mv = matrix_line;
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    matrix_line = GetMatrixLineVarious(number_of_colums, 0, 110,
                                                       "массива ME");
                    mfo_me = matrix_line;
                }
                //adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
                break;
            case 8:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                                   "массива MV");
                    mfi_mv = matrix_line;
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    matrix_line = GetMatrixLineVarious(number_of_colums, 1, 110,
                                                       "массива ME");
                    mfi_me = matrix_line;
                }
                //adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
                break;
            case 9:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 1, 7,
                                                                   "массива MV");
                    bmfo_mv = matrix_line;
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    matrix_line = GetMatrixLineVarious(number_of_colums, 1, 110,
                                                       "массива ME");
                    bmfo_me = matrix_line;
                }
                //adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
                break;
            case 10:
                number_of_strings = 1;
                cout << "Введите размер массива BFO:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива BFO");
                std::cout << "─────────────────────────────────────────────\n";
                for (int i = 0; i < number_of_strings ; ++i) {
                    vector<int> matrix_line = GetMatrixLineVarious(number_of_colums, 0, 7,
                                                                   "массива BFO");
                    bfo_fo = matrix_line;
                }
                //adjacency_matrix = ParseFromBF0(bfo_fo);
                break;
        }
        SetAdjacencyMatrix(graph, adjacency_matrix);
        std::cout << "Матрица установлена!\n";
    } else {
        std::cout << "─────────────────────────────────────────────\n";
        cout << "Ввод будет производится через файл.\n";
        cout << "Выберем тип ввода!";
        std::cout << "\n─────────────────────────────────────────────\n";
        cout << "[1] Матрица смежности\n[2] Матрица инцидентности\n[3] Список смежности\n[4]"
                " Список ребер\n[5] FO\n[6] FI\n[7] MFO\n[8] MFI\n[9] BMFO\n[10] BFO"
                "\n";
        std::cout << "─────────────────────────────────────────────\n";
        int32_t choice_second = GetChoiceVarious(10);

        vector<vector<int>> adjacency_matrix{};
        vector<vector<int>> incidence_matrix{};
        vector<vector<int>> adjacency_list{};
        vector<vector<int>> ribs_list{};
        vector<int> fo{};
        // FI представление.
        vector<int> fi{};
        // MFO представление.
        vector<int> mfo_me{};
        vector<int> mfo_mv{};
        // MFI представление.
        vector<int> mfi_me{};
        vector<int> mfi_mv{};
        // BMFO представление.
        vector<int> bmfo_me{};
        vector<int> bmfo_mv{};
        // BFO представление.
        vector<int> bfo_fo{};

        int32_t size_of_matrix = 0;
        int32_t number_of_strings = 0;
        int32_t number_of_colums = 0;
        switch (choice_second) {
            case 1:
                GetAdjasmentMatrixFromFile(graph);
                return;
                break;
            case 2:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество ребер:\n";
                number_of_colums = GetChoiceVarious(50, "количество рёбер");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    for (auto & i : string_data) {
                        vector<int> final;
                        vector<string> str = Split(i, ' ');
                        if (str.size() != number_of_colums) {
                            cerr << "Размер одной из строк не соответствует измерениям"
                                    ".\nНовый граф не установлен!\n";
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
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromIncidenceMatrix(incidence_matrix, number_of_strings);
                size_of_matrix = number_of_strings;
                break;
            case 3:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    int counter = 0;
                    for (auto & i : string_data) {
                        cout << "Введите количество смежных вершин для вершины."<< ++counter
                        << "\n";
                        number_of_colums = GetChoiceVarious(6, "количество смежных вершин");
                        vector<int> final;
                        vector<string> str = Split(i, ' ');
                        if (str.size() != number_of_colums) {
                            cerr << "Размер одной из строк не соответствует измерениям!"
                                    "\nНовый граф не установлен!\n";
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
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                size_of_matrix = number_of_strings;
                //adjacency_matrix = ParseFromAdjacencyList(adjacency_list, number_of_strings);
                break;
            case 4:
                cout << "Введите количество дуг/ребер для списка ребер:\n";
                number_of_strings = GetChoiceVarious(50, "количество дуг/ребер");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    for (auto & i : string_data) {
                        number_of_colums = 2;
                        vector<int> final;
                        vector<string> str = Split(i, ' ');
                        if (str.size() != number_of_colums) {
                            cerr << "Размер одной из строк не соответствует измерениям!"
                                    "\nНовый граф не установлен!\n";
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
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromRibsList(ribs_list, number_of_strings);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 5:
                number_of_strings = 1;
                cout << "Введите размер массива FO:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива FO");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    for (auto & i : string_data) {
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
                            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                            return;
                        }
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromFO(fo);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 6:
                number_of_strings = 1;
                cout << "Введите размер массива FI:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива FI");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    for (auto & i : string_data) {
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
                            cerr << "Возникли проблемы с валидностью строки!\n"
                                    "Новый граф не установлен!\n";
                            return;
                        }
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromFI(fi);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 7:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != 2) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    vector<int> final;
                    vector<string> str = Split(string_data[0], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                "\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfo_mv = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    str = Split(string_data[1], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                "\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfo_me = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromMFO(mfo_mv, mfo_me, number_of_strings);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 8:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != 2) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    vector<int> final;
                    vector<string> str = Split(string_data[0], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                "\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfi_mv = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    str = Split(string_data[1], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                "\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfi_me = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromMFI(mfi_mv, mfi_me, number_of_strings);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 9:
                cout << "Введите количество вершин:\n";
                number_of_strings = GetChoiceVarious(7, "количество вершин");
                std::cout << "─────────────────────────────────────────────\n";
                cout << "Введите количество элементов массива MV:\n";
                number_of_colums = GetChoiceVarious(20, "количество элементов массива MV");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != 2) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    vector<int> final;
                    vector<string> str = Split(string_data[0], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                "\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfi_mv = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                    cout << "Введите количество элементов массива ME:\n";
                    number_of_colums = GetChoiceVarious(100, "количество элементов массива ME");
                    std::cout << "─────────────────────────────────────────────\n";
                    str = Split(string_data[1], ' ');
                    if (str.size() != number_of_colums) {
                        cerr << "Размер строки не соответствует измерениям!"
                                ".\nНовый граф не установлен!\n";
                        return;
                    }
                    if (IsValidForMatrix(string_data, final, 0, 7)) {
                        mfi_me = final;
                        final = {};
                    } else {
                        cerr << "Возникли проблемы с валидностью строки!\n"
                                "Новый граф не установлен!\n";
                        return;
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromBMFO(bmfo_mv, bmfo_me, number_of_strings);
                size_of_matrix = adjacency_matrix.size();
                break;
            case 10:
                number_of_strings = 1;
                cout << "Введите размер массива BFO:\n";
                number_of_colums = GetChoiceVarious(100, "размер массива BFO");
                std::cout << "─────────────────────────────────────────────\n";
                try {
                    vector<string> string_data = ReadAllLinesInFile();
                    if (string_data.size() != number_of_strings) {
                        cerr << "Количество строк не подходит!\nНовый граф не установлен!\n";
                        return;
                    }
                    for (auto & i : string_data) {
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
                            cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                            return;
                        }
                    }
                } catch (exception&) {
                    cerr << "Произошла ошибка при считывании файла!";
                    return;
                }
                //adjacency_matrix = ParseFromBF0(bfo_fo);
                size_of_matrix = adjacency_matrix.size();
                break;
        }
        CheckAndSetFileData(graph, size_of_matrix, adjacency_matrix);
        std::cout << "Матрица установлена!\n";
    }
}

void GetAdjasmentMatrixFromFile(Graph &graph) {
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
            if (IsValidForMatrix(str, final)) {
                adjacency_matrix.push_back(final);
                final = {};
            } else {
                cerr << "Возникли проблемы с валидностью строки!\nНовый граф не установлен!\n";
                return;
            }
        }
        CheckAndSetFileData(graph, size_of_matrix, adjacency_matrix);
    } catch (exception& e) {
        cerr << "Произошла неизвестная ошибка при чтении из файла!\n";
        return;
    }
}

void CheckAndSetFileData(Graph &graph, int32_t size_of_matrix,
                         const vector<vector<int>> &adjacency_matrix) {
    if (!adjacency_matrix.empty() && adjacency_matrix.size() == size_of_matrix) {
        SetAdjacencyMatrix(graph, adjacency_matrix);
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
    string line = ReadFileIntoString("..\\Input.txt");
    vector<string> string_data = SplitSegment(line);
    cout << "<< Начало содержимого файла >>\n";
    int counter_strings = 1;
    for (auto & i : string_data) {
        cout<< counter_strings++ << "|\t" << i << endl;
    }
    cout << "<< Конец содержимого файла >>\n";
    return string_data;
}

void SetAdjacencyMatrix(Graph &graph, const vector<vector<int>>& adjacency_matrix) {
    graph.adjacency_matrix = adjacency_matrix;
    graph.Exist = true;
    graph.Oriented = IsOriented(adjacency_matrix);
    graph.WithLoops = (CountLoops(adjacency_matrix) > 0);
    graph.Pseudo = IsPseudo(adjacency_matrix, graph.Oriented);
    graph.Multi = IsMulti(adjacency_matrix);
    graph.type = "Матрица смежности";
    graph.p = static_cast<int>(adjacency_matrix.size());
    graph.q = CountRibs(adjacency_matrix);
    graph.arcs = CountArcs(adjacency_matrix);
    graph.loops = CountLoops(adjacency_matrix);
    cout << GetStringAdjacencyMatrix(graph);
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
    std::cout << "─────────────────────────────────────────────\n";
    cout << "Приступаем к выводу графа в текущем представлении";
    std::cout << "\n─────────────────────────────────────────────\n";
    cout << "[1] В консоль\n[2] В файл Output.txt (В одной директории с CmakeList.txt)\n";
    std::cout << "─────────────────────────────────────────────\n";
    int32_t choice = GetChoiceVarious(2);
    std::cout << "˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅˅\n";
    if (choice == 1) {
        PrintMatrixRepresentation(graph);
    } else {
        try {
            char * fileName = "..\\Output.txt";
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
    if (graph.Pseudo && graph.Oriented) {
        cout << "Для ориентированного псеводографа доступны следующие представления.\n";
        cout << "[1] Матрица смежности\n"
                "[2] Список смежности\n"
                "[3] Список ребёр\n"
                "[4] FO\n"
                "[5] FI\n"
                "[6] MFO\n";
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
    } else if (graph.Pseudo && !graph.Oriented) {
        cout << "Для неориентированного псеводографа доступны следующие представления.\n";
        cout << "[1] Матрица смежности\n"
                "[2] Список смежности\n"
                "[3] Список ребёр\n"
                "[4] FO\n"
                "[5] MFO\n";
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
    } else if (graph.Multi && !graph.Oriented) {
        cout << "Для неориентированного мультиграфа доступны следующие представления.\n";
        cout << "[1] Матрица смежности\n"
                "[2] Список смежности\n"
                "[3] Список ребёр\n"
                "[4] FO\n"
                "[5] MFO\n"
                "[6] BFO\n"
                "[7] BMFO\n";
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
    } else if (!graph.Oriented) {
        cout << "Для неориентированного графа доступны следующие представления.\n";
        cout << "[1] Матрица смежности\n"
                "[2] Матрица инцидентности\n"
                "[3] Список смежности\n"
                "[4] Список ребёр\n"
                "[5] FO\n"
                "[6] MFO\n"
                "[7] BFO\n"
                "[8] BMFO\n";
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
    } else {
        cout << "Для ориентированного графа доступны следующие представления.\n";
        cout << "[1] Матрица смежности\n"
                "[2] Матрица инцидентности\n"
                "[3] Список смежности\n"
                "[4] Список ребёр\n"
                "[5] FO\n"
                "[6] FI\n"
                "[7] MFO\n"
                "[8] MFI\n";
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
    vector<vector<int>> adjacency_matrix = graph.adjacency_matrix;
    vector<int> used(adjacency_matrix.size()); // вершины графа
    vector<int> path;
    for (int & i : used) i = 0;
    stack.push(0);
    while (!stack.empty())
    {
        int now_point = stack.top(); // извлекаем вершину
        stack.pop();
        if (used[now_point] == 2) continue;
        if (path.empty()) cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else cout << "Путь: " << path[path.size()-1] << " --> " << now_point + 1 << "\n";
        cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = adjacency_matrix.size()-1; j >= 0; j--)
        {
            if (adjacency_matrix[now_point][j] == 1 && used[j] != 2)
            {
                cout << "Обнаружили смежную вершину " << j+1<< "\n";
                stack.push(j);
                used[j] = 1;
                path.push_back(now_point + 1);
            }
        }
        cout << ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n"; // выводим номер вершины
    }
    cout << "Обход в глубину стандартным методом завершён!\n";
    std::cout << "˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄˄\n\n";
}

void DFSSearchRecursion(int start, int end, const vector<vector<int>>& matrix,
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
    DFSSearchRecursion(0, graph.adjacency_matrix.size(), graph.adjacency_matrix, used, path);
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
    vector<int> used(adjacency_matrix.size()); // вершины графа
    vector<int> path;
    for (int & i : used) i = 0;
    queue.push(0);
    while (!queue.empty())
    {
        int now_point = queue.front(); // извлекаем вершину
        queue.pop();
        if (used[now_point] == 2) continue;
        if (path.empty()) cout << "Cтартовая точка: " << now_point + 1 << "\n";
        else cout << "Путь: " << path[path.size()-1] << " --> " << now_point + 1 << "\n";
        cout << "Посетили вершину " << now_point + 1 << "\n";
        used[now_point] = 2;
        for (int j = 0; j < adjacency_matrix.size(); j++)
        {
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
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix[i].size(); ++j) {
            if (i != j && matrix[i][j] != matrix[j][i]) {
                return true;
            }
        }
    }
    return false;
}

bool IsMulti(const vector<vector<int>> &matrix) {
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
    int count = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix[i].size(); ++j) {
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
    int count = 0;
    int anti_count = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix[i].size(); ++j) {
            if (i == j) {
                count += matrix[i][j];
            } else if (matrix[i][j] > 0 && matrix[j][i] == matrix[i][j]) {
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

std::vector<std::vector<int>> ParseInIncidenceMatrix(const std::vector<std::vector<int>>& adjacency_list) {
    std::vector<std::vector<int>> incidence;
    std::set<std::pair<int, int>> rejected;
    for (int i = 0; i < adjacency_list.size(); ++i) {
        for (int j = 0; j < adjacency_list[i].size(); ++j) {
            if (!rejected.contains({i, adjacency_list[i][j]-1})) {
                std::vector<int> work_flow(adjacency_list.size());
                auto pointer = find(adjacency_list[adjacency_list[i][j]-1].begin(),
                                    adjacency_list[adjacency_list[i][j]-1].end(), i+1);
                if (pointer == adjacency_list[adjacency_list[i][j]-1].end()) {
                    int path = std::count(adjacency_list[adjacency_list[i][j]-1].begin(),
                                          adjacency_list[adjacency_list[i][j]-1].end(), i+1);
                    work_flow[i] = 1;
                    work_flow[adjacency_list[i][j]-1] = -1;
                } else {
                    work_flow[i] = 1;
                    work_flow[adjacency_list[i][j]-1] = 1;
                }
                rejected.insert({adjacency_list[i][j]-1, i});
                incidence.push_back(work_flow);
            }
        }
    }
    std::vector<std::vector<int>> new_matrix(adjacency_list.size());
    for (std::size_t i = 0; i < adjacency_list.size(); i++){
        for (int j = 0; j < incidence.size(); ++j) {
            new_matrix[i].push_back(incidence[j][i]);
        }
    }
    return new_matrix;
}

std::vector<std::vector<int>> ParseInAdjacencyList(const std::vector<std::vector<int>>& matrix) {
    std::vector<std::vector<int>> adjacency_list(matrix.size());
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    adjacency_list[i].push_back(j+1);
                    --num_of_path;
                }
            }
        }
    }
    return adjacency_list;
}

std::vector<std::vector<int>> ParseInRibsList(const std::vector<std::vector<int>>& matrix, int32_t arcs) {
    std::vector<std::vector<int>> ribs_list(arcs);
    int32_t ribs_count = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    ribs_list[ribs_count].push_back(i+1);
                    ribs_list[ribs_count].push_back(j+1);
                    ++ribs_count;
                    --num_of_path;
                }
            }
        }
    }
    return ribs_list;
}

std::vector<int> ParseInFO(const std::vector<std::vector<int>>& matrix, bool oriented) {
    std::vector<int> fo;
    fo.push_back(static_cast<int>(matrix.size()));
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    fo.push_back(j + 1);
                    --num_of_path;
                }
            }
        }
        fo.push_back(0);
    }
    return fo;
}

std::vector<int> ParseInFI(const std::vector<std::vector<int>>& matrix, bool oriented) {
    std::vector<int> fi;
    if (!oriented) {
        return {};
    }
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    fi.push_back(static_cast<int>(new_matrix.size()));
    for (int i = 0; i < new_matrix.size(); ++i) {
        for (int j = 0; j < new_matrix.size(); ++j) {
            if (new_matrix[i][j] > 0) {
                int num_of_path = new_matrix[i][j];
                while (num_of_path != 0) {
                    fi.push_back(j + 1);
                    --num_of_path;
                }
            }
        }
        fi.push_back(0);
    }
    return fi;
}

std::pair<std::vector<int>, std::vector<int>> ParseInMFO(const std::vector<std::vector<int>>& matrix, bool oriented) {
    std::vector<int> me;
    std::vector<int> mv;
    int last = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    me.push_back(j + 1);
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

std::pair<std::vector<int>, std::vector<int>> ParseInMFI(const std::vector<std::vector<int>>& matrix, bool oriented){
    if (!oriented) {
        return {{}, {}};
    }
    std::vector<int> me(0);
    std::vector<int> mv(0);
    std::vector<std::vector<int>> new_matrix(matrix.size());
    for (std::size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    int last = 0;
    for (int i = 0; i < new_matrix.size(); ++i) {
        for (int j = 0; j < new_matrix.size(); ++j) {
            if (new_matrix[i][j] > 0) {
                int num_of_path = new_matrix[i][j];
                while (num_of_path > 0) {
                    me.push_back(j + 1);
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
    if (oriented) {
        return {};
    }
    std::vector<int> fo;
    fo.push_back(static_cast<int>(matrix.size()));
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    if (j >= i) {
                        fo.push_back(j + 1);
                    }
                    --num_of_path;
                }
            }
        }
        fo.push_back(0);
    }
    return fo;
}

std::pair<std::vector<int>, std::vector<int>> ParseInBMFO(const std::vector<std::vector<int>>& matrix, bool oriented) {
    std::vector<int> me;
    std::vector<int> mv;
    int last = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    if (j >= i) {
                        me.push_back(j + 1);
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
    do {
        try {
            std::getline(std::cin, line);
            ans = stoi(line);
        } catch (std::exception& exception){
            std::cin.clear();
        }
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
    std::string line;
    std::cout << "Выбретите опцию, отправив число от 1 до "<< upper_bound << ".\n";
    do {
        try {
            std::getline(std::cin, line);
            ans = stoi(line);
        } catch (std::exception& exception){
            std::cin.clear();
        }
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
    do {
        try {
            std::getline(std::cin, line);
            ans = stoi(line);
        } catch (std::exception& exception){
            std::cin.clear();
        }
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
        ans = stoi(s);
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

std::ifstream::pos_type GetFileSize(const string& filename)
{
    std::ifstream in(filename, std::ifstream::ate | std::ifstream::binary);
    return in.tellg();
}

string ReadFileIntoString(const string& path) {
    ifstream input_file(path);
    try {
        int size = 0;
        size = GetFileSize(path);
        if (size > 30000) {
            cerr << "Отмена чтения. Файл весит больше 30 КБ!\n";
            return "";
        }
        if (!input_file.is_open()) {
            cerr << "Невозможно открыть файл!\n";
            return "";
        }
        return string((std::istreambuf_iterator<char>(input_file)),
                      std::istreambuf_iterator<char>());
    } catch (exception) {
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
            int check = stoi(line);
            string_data = Split(line, ' ');
            if (string_data.size() != size_of_matrix) {
                cout << "Размер не подходит!\n";
            } else {
                if (IsValidForMatrix(string_data, final)) {
                    cout << "Успешно получена строка матрицы!\n";
                    return final;
                }
            }
        } catch (exception e) {
            cout << "Введите подходящую строку!\n";
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
        while (std::getline(str_stream, item, delim)) {
            if (item[0] != delim && item != "") ans.push_back(item);
        }
        return ans;
    } catch (exception&) {
        cerr << "Не вводите пустую строку!\n";
        cin.clear();
    }
    return ans;
}

vector<string> SplitSegment(const string& sentence) {
    vector<string> ans;
    try {
        std::stringstream str_stream(sentence);
        std::string item;
        while (std::getline(str_stream, item, '\n')) {
            if (item != " " && item != "") ans.push_back(item);
        }
    } catch (exception&) {
        cerr << "Возникла ошибка при разделении файла на строки!\n";
    }
    return ans;
}

bool IsValidString(const string& s) {
    int32_t ans = 0;
    try {
        ans = stoi(s);
        if (ans >= 0 && ans <= 4) {
            return true;
        } else {
            cerr << "Одно из чисел не в пределах от 0 до 4!\n";
            return false;
        }
    } catch (std::exception& exception){
        cerr << "Данные не содержат число!\n";
        return false;
    }
    return ans;
}

bool IsValidForMatrix(const vector<string>& line, vector<int>& ans){
    for (auto & i : line) {
        if (!IsValidString(i)) {
            ans = {};
            return false;
        } else {
            ans.push_back(ParseNum(i));
        }
    }
    return true;
}

bool IsValidString(const string& s, int32_t lower_bound, int32_t upper_bound) {
    int32_t ans = 0;
    try {
        ans = stoi(s);
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
        ans = stoi(s);
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
        if (!IsValidString(i, lower_bound, upper_bound)) {
            ans = {};
            return false;
        } else {
            ans.push_back(ParseNum(i, lower_bound, upper_bound));
        }
    }
    return true;
}

vector<int> GetMatrixLineVarious(int32_t size_of_matrix, int32_t lower_bound, int32_t upper_bound,
                          string comment) {
    string line;
    bool flag = false;
    vector<int> final;
    vector<string> string_data;
    do {
        cout << "Введите строку " << comment << " размером " << size_of_matrix << endl;
        try {
            getline(cin, line);
            int check = stoi(line);
            string_data = Split(line, ' ');
            if (string_data.size() != size_of_matrix) {
                cout << "Размер не подходит!\n";
            } else {
                if (IsValidForMatrix(string_data, final, lower_bound, upper_bound)) {
                    cout << "Успешно получена строка " << comment << "!\n";
                    return final;
                }
            }
        } catch (exception e) {
            cout << "Введите подходящую строку!\n";
            std::cin.clear();
            continue;
        }
    } while (!flag);
    return final;
}

