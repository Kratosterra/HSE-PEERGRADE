#include <iostream>  // Библиотека ввода-вывода.
#include <vector>
#include <cassert>
#include <set>
#include "Functions.h" // Указываем заголовочный файл;
#include <stdio.h>
#include <windows.h>

using namespace std;  // Укажем использование пространства имен std.


struct Graph {
    bool Exist = true;
    vector<vector<int>> adjacency_matrix = {{0, 1, 0, 0, 1},
                                            {1, 0, 1, 1, 1},
                                            {0, 1, 0, 1, 0},
                                            {0, 1, 1, 0, 1},
                                            {1, 1, 0, 1, 0}};
    bool Oriented = IsOriented(adjacency_matrix);
    bool WithLoops = (CountLoops(adjacency_matrix) > 0);
    bool Pseudo = IsPseudo(adjacency_matrix, Oriented);
    bool Multi = IsMulti(adjacency_matrix);
    string type = "BMFO";
    int32_t p = static_cast<int>(adjacency_matrix.size());
    int32_t q = CountRibs(adjacency_matrix);
    int32_t arcs = CountArcs(adjacency_matrix);
    int32_t loops = CountLoops(adjacency_matrix);
    vector<vector<int>> vertex_degrees = GetVertexDegrees(adjacency_matrix, WithLoops, Oriented);
    vector<vector<int>> incidence_matrix = ParseInIncidenceMatrix(adjacency_matrix, Pseudo ? arcs
    : q);
    vector<vector<int>> adjacency_list = ParseInAdjacencyList(adjacency_matrix);
    vector<vector<int>> ribs_list = ParseInRibsList(adjacency_matrix, arcs);
    vector<int> fo = ParseInFO(adjacency_matrix, Oriented);
    vector<int> fi = ParseInFI(adjacency_matrix, Oriented);
    vector<int> mfo_me = ParseInMFO(adjacency_matrix, Oriented).first;
    vector<int> mfo_mv = ParseInMFO(adjacency_matrix, Oriented).second;
    vector<int> mfi_me = ParseInMFI(adjacency_matrix, Oriented).first;
    vector<int> mfi_mv = ParseInMFI(adjacency_matrix, Oriented).second;
    vector<int> bmfo_me = ParseInBMFO(adjacency_matrix, Oriented).first;
    vector<int> bmfo_mv = ParseInBMFO(adjacency_matrix, Oriented).second;
    vector<int> bfo_fo = ParseInBFO(adjacency_matrix, Oriented);
};

string GetStringAdjacencyMatrix(const Graph &graph);

string GetStringIncidenceMatrix(const Graph &graph);

string GetStringAdjacencyList(const Graph &graph);

string GetStringRibsList(const Graph &graph);

string GetStringFO(const Graph &graph);

string GetStringFI(const Graph &graph);

string GetStringMFO(const Graph &graph);

string GetStringMFI(const Graph &graph);

string GetStringBFO(const Graph &graph);

void PrintVertexDegrees(const Graph &graph);

string GetStringBMFO(const Graph &graph);

void PrintMatrixRepresentation(const Graph &graph);

void PrintGraphMainInfo(const Graph &graph);

/**
 *  @brief  Основная функция работы программы.
 *  @return  0 при успешном выполнении, -1 при возникновении ошибки.
*/

int main() {
    system("chcp 65001");
    const int32_t EXIT_NUM = 9;  // Переменная для проверки опции выхода из программы.
    int32_t now_choice;  // Текущий выбор меню пользователем.
    Graph graph; // Задаем граф.

    system("color 01");
    ShowStartOfProgram();  // Показваем заставку.
    do {
        system("color 01");
        ShowNowGraphInfo(graph);  // Показываем текущий граф.
        ShowMainMenu(); // Показываем стратовое меню.
        now_choice = GetChoice(); // Получаем выбор пользователя.
        // Если выбор является приемлемым - рабоатем.
        DoGraphWork(now_choice, graph);
    } while (now_choice != EXIT_NUM); // Пока человек не пожелал выйти продолжаем работу.
    ShowExitOfProgram(); // Показываем конечную заставку.

    return 0;
}

void ShowStartOfProgram() {
    std::cout << "Это заставка\n";
}

void ShowExitOfProgram() {
    std::cout << "Спасибо за использование!\n";
    system("color 01");
}

void ShowMainMenu() {
    std::cout << "Это главное меню!\n"
                 "-------------------------------------\n"
                 "[1] Ввод графа\n"
                 "[2] Вывод графа\n"
                 "[3] Смена типа представления/хранения\n"
                 "[4] Подсчет степеней/полустепеней вершин\n"
                 "[5] Подсчет ребер/дуг\n"
                 "[6] Обход графа в глубину (стандартно)\n"
                 "[7] Обход графа в глубину (рекурсивно)\n"
                 "[8] Обход графа в ширину\n"
                 "[9] Выйти из программы\n"
                 "-------------------------------------\n";
}

void ShowNowGraphInfo(const Graph& graph) {
    std::cout << "-------------------------------------\n"
                 "Информация о графе\n"
                 "-------------------------------------\n";
    if (!graph.Exist) {
        std::cout << "> Граф не существует, воспользуйтесь вводом.\n";
    } else  {
        PrintGraphMainInfo(graph);
        cout << "Представление:\n";
        PrintMatrixRepresentation(graph);
        PrintVertexDegrees(graph);
    }
    std::cout << "-------------------------------------\n";
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
                                                   "гаранитруется]" : "Нет петель")
                  << ".\n> Количество петель: " << graph.loops << "\n";
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

string GetStringBMFO(const Graph &graph) {
    string output;
    output += "ME: [";
    for (int i : graph.bmfo_me) {
        output += to_string(i) + ", ";
    }
    output += "]\n" ;
    output += "MV: [";
    for (int i : graph.bmfo_mv) {
        output += to_string(i) + ", ";
    }
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
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

string GetStringBFO(const Graph &graph) {
    string output;
    output += "BFO: [";
    for (int i : graph.bfo_fo) {
        output += to_string(i) + ", ";
    }
    output += "]\n";
    return output;
}

string GetStringMFI(const Graph &graph) {
    string output;
    output += "ME: [";
    for (int i : graph.mfi_me) {
        output += to_string(i) + ", ";
    }
    output += "]\n" ;
    output += "MV: [";
    for (int i : graph.mfi_mv) {
        output += to_string(i) + ", ";
    }
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
}

string GetStringMFO(const Graph &graph) {
    string output;
    output += "ME: [";
    for (int i : graph.mfo_me) {
        output += to_string(i) + ", ";
    }
    output += "]\n" ;
    output += "MV: [";
    for (int i : graph.mfo_mv) {
        output += to_string(i) + ", ";
    }
    output += "]\nP: " + to_string(graph.p) + "\n";
    return output;
}

string GetStringFI(const Graph &graph) {
    string output;
    output += "FI: [";
    for (int i : graph.fi) {
        output += to_string(i) + ", ";
    }
    output += "]\n" ;
    return output;
}

string GetStringFO(const Graph &graph) {
    string output;
    output += "FO: [";
    for (int i : graph.fo) {
        output += to_string(i) + ", ";
    }
    output += "]\n" ;
    return output;
}

string GetStringRibsList(const Graph &graph) {
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
    string output;
    for (size_t i = 0; i < graph.adjacency_list.size(); ++i) {
        output += to_string(i+1) + ": ";
        for (int j : graph.adjacency_list[i]) {
            output += to_string(j) + ",";
        }
        output += "\n" ;
    }
    return output;
}

string GetStringIncidenceMatrix(const Graph &graph) {
    string output = "[НУМЕРАЦИЯ РЁБЕР ПРОИЗВОЛЬНАЯ]\n";
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
            // Игнорирование.
        }
        if (ans >= 1 && ans <= 9) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до 9!\n";
        }
    } while (!valid_choice);
    system("color 01");
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
            // Игнорирование.
        }
        if (ans >= 1 && ans <= upper_bound) {
            valid_choice = true;
        } else {
            system("color 0C");
            std::cout << "Ошибка, введите число от 1 до "<< upper_bound << "!\n";
        }
    } while (!valid_choice);
    system("color 01");
    return ans;
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
    cout << "Ввод!\n";
}

void OutputGraph(const Graph& graph) {
    std::cout << "-------------------------------------\n";
    cout << "Приступаем к выводу графа в текущем представлении\n";
    std::cout << "-------------------------------------\n";
    if (!graph.Exist) {
        cout << "Граф не существует. Воспользуйтесь вводом!\n";
        std::cout << "-------------------------------------\n";
        return;
    }

    cout << "[1] В консоль\n[2] В файл Output.txt\n";
    std::cout << "-------------------------------------\n";
    int32_t choice = GetChoiceVarious(2);
    if (choice == 1) {
        PrintMatrixRepresentation(graph);
    } else {
        cout << "Пока нет\n";
    }
    std::cout << "-------------------------------------\n";
    Sleep(100);
}

void ChangeTypeOfGraph(Graph& graph) {
    cout << "Смена типа представления/хранения!\n";
}

void CountDegreesOfVertices(const Graph& graph) {
    cout << "Подсчет степеней!\n";
}

void CountEdgesOfGraph(const Graph& graph) {
    cout << "Подсчет ребер!\n";
}

void DFSClassic(const Graph& graph) {
    cout << "В глубину обычно!\n";
}

void DFSRecursion(const Graph& graph) {
    cout << "В глубину рекурсивно!\n";
}

void BFS(const Graph& graph) {
    cout << "В ширину!\n";
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
                    count += 1;
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
            if (matrix[i][j] > 0 && matrix[j][i] == matrix[i][j]) {
                count += matrix[i][j];
                anti_count += matrix[i][j];
            } else if (matrix[i][j] > 0) {
                count += matrix[i][j];
            }
        }
    }
    return count - (anti_count/2);
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

vector<vector<int>> ParseInIncidenceMatrix(const vector<vector<int>>& matrix, int32_t arcs) {
    vector<bool> matrix_row;
    vector<vector<int>> incidence(matrix.size());
    for (auto & i : incidence) {
        i = vector<int>(arcs);
    }
    set<pair<int, int>> rejected;
    int id_of_rib = 0;
    for (int i = 0; i < matrix.size(); ++i) {
       for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] >= 1) {
                int num_of_path = matrix[i][j];
                if (!rejected.contains({i, j})) {
                    while (num_of_path != 0) {
                        if (matrix[i][j] == matrix[j][i]) {
                            incidence[i][id_of_rib] = 1;
                            incidence[j][id_of_rib] = 1;
                        } else {
                            incidence[i][id_of_rib] = 1;
                            incidence[j][id_of_rib] = -1;
                        }
                        ++id_of_rib;
                        --num_of_path;
                    }
                    rejected.insert({j, i});
                }
           }
       }
    }
    return incidence;
}

vector<vector<int>> ParseInAdjacencyList(const vector<vector<int>>& matrix) {
    vector<vector<int>> adjacency_list(matrix.size());
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

vector<vector<int>> ParseInRibsList(const vector<vector<int>>& matrix, int32_t arcs) {
    vector<vector<int>> ribs_list(arcs);
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

vector<int> ParseInFO(const vector<vector<int>>& matrix, bool oriented) {
    vector<int> fo;
    fo.push_back(matrix.size());
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

vector<int> ParseInFI(const vector<vector<int>>& matrix, bool oriented) {
    vector<int> fi;
    if (!oriented) {
        return {};
    }
    vector<vector<int>> new_matrix(matrix.size());
    for (size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
                    .push_back(j[i]);
    fi.push_back(new_matrix.size());
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

pair<vector<int>, vector<int>> ParseInMFO(const vector<vector<int>>& matrix, bool oriented) {
    vector<int> me;
    vector<int> mv;
    int last = 0;
    for (int i = 0; i < matrix.size(); ++i) {
        for (int j = 0; j < matrix.size(); ++j) {
            if (matrix[i][j] > 0) {
                int num_of_path = matrix[i][j];
                while (num_of_path != 0) {
                    me.push_back(j + 1);
                    --num_of_path;
                }
                last = me.size();
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}

pair<vector<int>, vector<int>> ParseInMFI(const vector<vector<int>>& matrix, bool oriented){
    if (!oriented) {
        return {{}, {}};
    }
    vector<int> me(0);
    vector<int> mv(0);
    vector<vector<int>> new_matrix(matrix.size());
    for (size_t i = 0; i < matrix.size(); i++) for (auto & j : matrix) new_matrix[i]
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
                last = me.size();
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}

vector<int> ParseInBFO(const vector<vector<int>>& matrix, bool oriented) {
    if (oriented) {
        return {};
    }
    vector<int> fo;
    fo.push_back(matrix.size());
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

pair<vector<int>, vector<int>> ParseInBMFO(const vector<vector<int>>& matrix, bool oriented) {
    vector<int> me;
    vector<int> mv;
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
                last = me.size();
            }
        }
        mv.push_back(last);
        last = 0;
    }
    return {me, mv};
}