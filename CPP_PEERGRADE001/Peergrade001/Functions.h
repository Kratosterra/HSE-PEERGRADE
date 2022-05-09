#include <iostream>
#include <vector>
#include <set>
#include <limits>
#include <Windows.h>
#include <cstdio>
#include <sstream>
#include <fstream>
#include <cmath>
#include <stack>
#include <queue>

/**
 *  @brief  Структура представления графа.
*/

struct Graph;

/**
 *  @brief  Функция, показывающая начальную заставку.
*/

void ShowStartOfProgram();

/**
 *  @brief  Функция, показывающая конечную заставку.
*/

void ShowExitOfProgram();

/**
 *  @brief  Функция, показывающая главное меню.
*/

void ShowMainMenu();

/**
 *  @brief  Функция, которая показывает информацию о текущем графе.
 *  @param  graph  Текущий граф.
*/

void ShowNowGraphInfo(const Graph& graph);

/**
 *  @brief  Функция, которая получает выбор пользователя.
 *  @return  Возвращает значение в пределах от 1 до 8 включительно.
*/

int32_t GetChoice();


/**
 *  @brief  Функция, которая запускает выполнение действий с графом.
 *  @param  now_choice  Текущая опция действий.
 *  @param  graph  Текущий граф.
*/

void DoGraphWork(int32_t now_choice, Graph& graph);

/**
 *  @brief  Функция, которая получает граф от пользователя.
 *  @param  graph  Текущий граф.
*/

void GetNewGraph(Graph& graph);

/**
 *  @brief  Функция, которая выводит граф.
 *  @param  graph  Текущий граф.
*/

void OutputGraph(const Graph& graph);

/**
 *  @brief  Функция, которая меняет тип представления графа.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraph(Graph& graph);

/**
 *  @brief  Функция, которая считает степени/полустепени вершин графа.
 *  @param  graph  Текущий граф.
*/

void CountDegreesOfVertices(const Graph& graph);

/**
 *  @brief  Функция, которая считает ребра или дуги графа.
 *  @param  graph  Текущий граф.
*/

void CountEdgesOfGraph(const Graph& graph);

/**
 *  @brief  Функция, которая обходит граф в глубину стандартно.
 *  @param  graph  Текущий граф.
*/

void DFSClassic(const Graph& graph);

/**
 *  @brief  Функция, которая обходит граф в глубину рекурсивно.
 *  @param  graph  Текущий граф.
*/

void DFSRecursion(const Graph& graph);

/**
 *  @brief  Функция, проверяющая мультиграф ли граф.
 *  @param start Начальная вершина поиска.
 *  @param end Конечная вершина поиска
 *  @param matrix Матрица смежности графа.
 *  @param used Использованные вершины.
 *  @param path Путь прохождения по врешинам.
*/

void DFSSearchRecursion(int start, int end, const std::vector<std::vector<int>>& matrix,
                        std::vector<int>& used, std::vector<int>& path);

/**
 *  @brief  Функция, которая обходит граф в ширину.
 *  @param  graph  Текущий граф.
*/

void BFS(const Graph& graph);

/**
 *  @brief  Функция, проверяющая ориентированый ли граф.
 *  @param  matrix  Матрица смежности графа.
 *  @return  Boolean значение является ли граф ориентированным.
*/

bool IsOriented(std::vector<std::vector<int>> matrix);

/**
 *  @brief  Функция, проверяющая мультиграф ли граф.
 *  @param  matrix  Матрица смежности графа.
 *  @return  Boolean значение является ли граф мультиграфом.
*/

bool IsMulti(const std::vector<std::vector<int>> &matrix);

/**
 *  @brief  Функция, проверяющая пвсевдограф ли граф.
 *  @param  matrix  Матрица смежности графа.
 *  @param  oriented  Ориентированный ли граф.
 *  @return Boolean значение является ли граф псевдографом.
*/

bool IsPseudo(const std::vector<std::vector<int>>& matrix, bool oriented);

/**
 *  @brief  Функция, считающая количество петель.
 *  @param  matrix Матрица смежности графа.
 *  @return Количество петель графа.
*/

int CountLoops(std::vector<std::vector<int>> matrix);

/**
 *  @brief  Функция, считающая дуги графа.
 *  @param  matrix  Матрица смежности графа.
 *  @return Количество дуг графа.
*/

int CountArcs(const std::vector<std::vector<int>>& matrix);

/**
 *  @brief  Функция, считающая рёбра графа.
 *  @param  matrix  Матрица смежности графа.
 *  @return Количество ребёр графа.
*/

int CountRibs(std::vector<std::vector<int>> matrix);

/**
 *  @brief  Функция, превращающая список смежности в матрицу инсидентности.
 *  @param  adjacency_list  Список смежности графа.
 *  @return Mатрицу инcидентности.
*/

std::vector<std::vector<int>> ParseInIncidenceMatrix(
        const std::vector<std::vector<int>>& adjacency_list);

/**
 *  @brief  Функция, превращающая матрицу смежности в список смежности.
 *  @param  matrix Матрица смежности графа.
 *  @return Список смежности.
*/

std::vector<std::vector<int>> ParseInAdjacencyList(const std::vector<std::vector<int>>& matrix);

/**
 *  @brief  Функция, превращающая матрицу смежности в список ребер.
 *  @param  matrix Матрица смежности графа.
 *  @param  arcs  Количество дуг вграф.
 *  @return Список ребер.
*/

std::vector<std::vector<int>> ParseInRibsList(const std::vector<std::vector<int>>& matrix,
                                              int arcs);
/**
 *  @brief  Функция, превращающая матрицу смежности в FO представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return FO представление.
*/

std::vector<int> ParseInFO(const std::vector<std::vector<int>>& matrix, bool oriented);

/**
 *  @brief  Функция, превращающая матрицу смежности в FI представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return FI представление.
*/

std::vector<int> ParseInFI(const std::vector<std::vector<int>>& matrix, bool oriented);

/**
 *  @brief  Функция, превращающая матрицу смежности в MFO представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return Пару MV и ME для MFO представления.
*/

std::pair<std::vector<int>, std::vector<int>> ParseInMFO(const std::vector<std::vector<int>>& matrix,
                                                         bool oriented);
/**
 *  @brief  Функция, превращающая матрицу смежности в MFI представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return Пару MV и ME для MFI представления.
*/

std::pair<std::vector<int>, std::vector<int>> ParseInMFI(const std::vector<std::vector<int>>& matrix,
                                                         bool oriented);
/**
 *  @brief  Функция, превращающая матрицу смежности в BFO представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return BFO представление.
*/

std::vector<int> ParseInBFO(const std::vector<std::vector<int>>& matrix, bool oriented);

/**
 *  @brief  Функция, превращающая матрицу смежности в BMFO представление.
 *  @param  matrix Матрица смежности графа.
 *  @param oriented Ориентированный ли граф.
 *  @return Пару MV и ME для BMFO представления.
*/

std::pair<std::vector<int>, std::vector<int>> ParseInBMFO(const std::vector<std::vector<int>>& matrix,
                                                          bool oriented);

/**
 *  @brief  Функция, возвращающая векторы с информацией о степянх или полустепенях графа.
 *  @param  matrix Матрица смежности графа.
 *  @param with_loops C петлями ли граф.
 *  @param oriented Ориентированный ли граф.
 *  @return Список вершин со степенями.
*/

std::vector<std::vector<int>> GetVertexDegrees(std::vector<std::vector<int>> matrix,
                                               bool with_loops, bool oriented);

/**
 *  @brief  Функция, получаяющая строку с информацией о графе.
 *  @param  size_of_matrix Размер строки.
 *  @return Вектор с числами.
*/

std::vector<int> GetMatrixLine(int32_t size_of_matrix);

/**
 *  @brief  Функция, делящая строку по разделителю.
 *  @param  str Строка.
 *  @param  delim Разделитель.
 *  @return Вектор подстрок на которые была поделена строка.
*/

std::vector<std::string> Split(const std::string& str, char delim);

/**
 *  @brief  Функция, получающая ограниченный ввод от пользователя в виде числа.
 *  @param  upper_bound Верхняя граница числа.
 *  @return Число, которое ввел пользователь.
*/

int32_t GetChoiceVarious(int32_t upper_bound);

/**
 *  @brief  Функция, получающая ограниченный ввод от пользователя в виде числа.
 *  @param  upper_bound Верхняя граница числа.
 *  @param info Дополнительная инофрмация для подсказок пользователю.
 *  @return Число, которое ввел пользователь.
*/

int32_t GetChoiceVarious(int32_t upper_bound, const std::string& info);

/**
 *  @brief  Функция, получающая размер файла.
 *  @param  filename Имя файла.
 *  @return Примерный размер файла в байтах.
*/

std::ifstream::pos_type GetFileSize(const std::string& filename);

/**
 *  @brief  Функция, получающая всю информацию из файла.
 *  @param  path Путь до файла.
 *  @return Строка из всей информации в файле.
*/

std::string ReadFileIntoString(const std::string& path);

/**
 *  @brief  Функция, проверяющая валиден ли набор строк как информация для построеня представления
 *  Также заполняет вектор чисел для одной строки представления.
 *  @param  line  Набор строк.
 *  @param  ans  Изменяемый вектор чисел с инормацией для одной строки представления.
 *  @return Boolean значение является ли данный набор строк подходящим для построения представления.
*/

bool IsValidForMatrix(const std::vector<std::string>& line, std::vector<int>& ans);

/**
 *  @brief  Функция, проверяющая валидена ли строка как представление числа.
 *  @param  str  Строка.
 *  @return Boolean значение является ли данная строка подходящей для построения представления.
*/

bool IsValidString(const std::string& str);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в текущем типе хранения.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление графа.
*/

std::string GetStringMatrixRepresentation(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в виде матрицы смежности.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы смежности графа.
*/

std::string GetStringAdjacencyMatrix(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в виде матрицы инсидентности.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы инсидентности графа.
*/

std::string GetStringIncidenceMatrix(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в виде cписка смежности.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы cписка смежности.
*/

std::string GetStringAdjacencyList(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в виде cписка ребер.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы cписка ребер.
*/

std::string GetStringRibsList(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе FO.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе FO.
*/

std::string GetStringFO(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе FI.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе FI.
*/

std::string GetStringFI(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе MFO.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе MFO.
*/

std::string GetStringMFO(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе MFI.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе MFI.
*/

std::string GetStringMFI(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе BFO.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе BFO.
*/

std::string GetStringBFO(const Graph &graph);

/**
 *  @brief  Функция, возвращающая строковое предствление графа в типе BMFO.
 *  @param  graph  Текущий граф.
 *  @return Строка-представление матрицы в типе MFI.
*/

std::string GetStringBMFO(const Graph &graph);

/**
 *  @brief  Функция, выводящая на экран статистику по степеням/полустепеням вершин графа.
 *  @param  graph  Текущий граф.
*/

void PrintVertexDegrees(const Graph &graph);

/**
 *  @brief  Функция, выводящая на экран статистику по степеням/полустепеням вершин графа.
 *  @param  graph  Текущий граф.
*/

void PrintMatrixRepresentation(const Graph &graph);

/**
 *  @brief  Функция, выводящая на экран основную информацию о графе.
 *  @param  graph  Текущий граф.
*/

void PrintGraphMainInfo(const Graph &graph);

/**
 *  @brief  Функция, устанавливающая матрицу смежности для графа.
 *  @param  graph  Текущий граф.
 *  @param  adjacency_matrix  Матрица смежности графа.
 *  @param  type  Тип графа.
*/

void SetAdjacencyMatrix(Graph &graph, const std::vector<std::vector<int>>& adjacency_matrix,
                        std::string type);


/**
 *  @brief  Функция, получающая число из строки.
 *  @param  string  Строка с числом.
 *  @return Число из строки.
*/

int32_t ParseNum(const std::string& string);

/**
 *  @brief  Функция, делящая строку по разделителям новой строки.
 *  @param  sentence Строка для разделения.
 *  @return Вектор строк по разделителю.
*/

std::vector<std::string> SplitSegment(const std::string& sentence);

/**
 *  @brief  Функция, получающая матрицу смежности из файла.
 *  @param  graph Текущий граф.
*/

void GetAdjacencyMatrixFromFile(Graph &graph);

/**
 *  @brief  Функция, проверяющая валидена ли строка как представление числа.
 *  @param  str  Строка.
 *  @param  lower_bound  Нижняя граница для числа от пользователя.
 *  @param  upper_bound  Верхняя граница для числа от пользователя.
 *  @return Boolean значение является ли данная строка подходящей для построения представления.
*/

bool IsValidString(const std::string& str, int32_t lower_bound, int32_t upper_bound);

/**
 *  @brief  Функция, превращающая строку в число.
 *  @param  str  Строка.
 *  @param  lower_bound  Нижняя граница для числа от пользователя.
 *  @param  upper_bound  Верхняя граница для числа от пользователя.
 *  @return Число.
*/

int32_t ParseNum(const std::string& str, int32_t lower_bound, int32_t upper_bound);

/**
 *  @brief  Функция, проверяющая валиден ли набор строк как информация для построения представления
 *  Также заполняет вектор чисел для одной строки представления.
 *  @param  line  Набор строк.
 *  @param  ans  Изменяемый вектор чисел с инормацией для одной строки представления.
 *  @param  lower_bound  Нижняя граница для числа от пользователя.
 *  @param  upper_bound  Верхняя граница для числа от пользователя.
 *  @return Boolean значение является ли данный набор строк подходящим для построения представления.
*/

bool IsValidForMatrix(const std::vector<std::string>& line, std::vector<int>& ans, int32_t lower_bound,
                      int32_t upper_bound);


/**
 *  @brief  Функция, получаяющая строку с информацией о графе.
 *  @param  size_of_matrix Размер строки.
 *  @param  lower_bound  Нижняя граница для числа от пользователя.
 *  @param  upper_bound  Верхняя граница для числа от пользователя.
 *  @param  comment  Комментарий-подсказка для пользователя.
 *  @return Вектор с числами.
*/

std::vector<int> GetMatrixLineVarious(int32_t size_of_matrix, int32_t lower_bound, int32_t upper_bound,
                                      const std::string& comment);

/**
 *  @brief  Функция, считывающая информацию из файла, делящая его на строки.
 *  @return Вектор строк из файла.
*/

std::vector<std::string> ReadAllLinesInFile();

/**
 *  @brief  Функция, устанавливающая матрицу смежности для графа полученого из файла.
 *  @param  graph  Текущий граф.
 *  @param  adjacency_matrix  Матрица смежности графа.
 *  @param  type  Тип графа.
*/

void CheckAndSetFileData(Graph &graph, int32_t size_of_matrix,
                         const std::vector<std::vector<int>> &adjacency_matrix, std::string type);

/**
 *  @brief  Функция, производящая смену представления для ориентированных псевдографов.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraphForPseudoOriented(Graph &graph);

/**
 *  @brief  Функция, производящая смену представления для неориентированных псевдографов.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraphForPseudoNotOriented(Graph &graph);

/**
 *  @brief  Функция, производящая смену представления для мультиграфов.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraphForMulti(Graph &graph);

/**
 *  @brief  Функция, производящая смену представления для неориентированных графов.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraphForNotOriented(Graph &graph);

/**
 *  @brief  Функция, производящая смену представления для ориентированных графов.
 *  @param  graph  Текущий граф.
*/

void ChangeTypeOfGraphForOriented(Graph &graph);

/**
 *  @brief  Функция, возвращающая индекс элемента в векторе.
 *  @param  vector Вектор чисел.
 *  @param  item  Число, которое нужно найти.
 *  @return Индекс элемента item или -1.
*/

int Find(std::vector<int> &vector, int item);

/**
 *  @brief  Функция, превращающая список смежности в матрицу смежности.
 *  @param  adjacency_list Список смежности.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromAdjacencyList(std::vector<std::vector<int>> adjacency_list,
                                                     int32_t vertices);

/**
 *  @brief  Функция, превращающая матрицу инцидентности в матрицу смежности.
 *  @param  incidence_matrix Матрица инцидентности.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromIncidenceMatrix(std::vector<std::vector<int>>
incidence_matrix, int32_t vertices);

/**
 *  @brief  Функция, превращающая список ребер в матрицу смежности.
 *  @param  ribs_list Список ребер.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromRibsList(std::vector<std::vector<int>> ribs_list, int32_t
vertices);

/**
 *  @brief  Функция, превращающая представление FO в матрицу смежности.
 *  @param  fo FO-представление.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromFO(std::vector<int> fo);

/**
 *  @brief  Функция, превращающая представление FI в матрицу смежности.
 *  @param  fi FI-представление.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromFI(std::vector<int> fi);

/**
 *  @brief  Функция, превращающая представление MFO в матрицу смежности.
 *  @param  mv MV-вектор.
 *  @param  me  ME-вектор.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromMFO(const std::vector<int>& mv, std::vector<int> me, int32_t
vertices);

/**
 *  @brief  Функция, превращающая представление MFI в матрицу смежности.
 *  @param  mv MV-вектор.
 *  @param  me  ME-вектор.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromMFI(const std::vector<int>& mv, std::vector<int> me, int32_t
vertices);

/**
 *  @brief  Функция, превращающая представление BMFO в матрицу смежности.
 *  @param  mv MV-вектор.
 *  @param  me  ME-вектор.
 *  @param  vertices  Количество вершин.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromBMFO(const std::vector<int>& mv, std::vector<int> me, int32_t
vertices);

/**
 *  @brief  Функция, превращающая представление BFO в матрицу смежности.
 *  @param  fo BFO-представление.
 *  @return Матрицу смежности для данного представления.
*/

std::vector<std::vector<int>> ParseFromBFO(std::vector<int> fo);

/**
 *  @brief  Функция, получающая граф из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetConsoleGraphInput(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде матрицы смежности из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetAdjacencyMatrixFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде матрицы инцендентности из консольного ввода
 *  пользователя.
 *  @param  graph  Текущий граф.
*/

void GetIncidenceMatrixFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде списка смежности из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetAdjacencyListFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде списка ребер из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetRibsListFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении FO из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetFOFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении FI из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetFIFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении MFO из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetMFOFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении MFI из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetMFIFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении BMFO из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetBMFOFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении BFO из консольного ввода пользователя.
 *  @param  graph  Текущий граф.
*/

void GetBFOFromConsole(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде матрицы инциндентности из файла.
 *  @param  graph  Текущий граф.
*/

void GetIncidenceMatrixFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде списка смежности из файла.
 *  @param  graph  Текущий граф.
*/

void GetAdjacencyListFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в виде списка ребер из файла.
 *  @param  graph  Текущий граф.
*/

void GetRibsListFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении FO из файла.
 *  @param  graph  Текущий граф.
*/

void GetFOFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении FI из файла.
 *  @param  graph  Текущий граф.
*/

void GetFIFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении MFO из файла.
 *  @param  graph  Текущий граф.
*/

void GetMFOFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении MFI из файла.
 *  @param  graph  Текущий граф.
*/

void GetMFIFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении BMFO из файла.
 *  @param  graph  Текущий граф.
*/

void GetBMFOFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф в представлении BFO из файла.
 *  @param  graph  Текущий граф.
*/

void GetBFOFromFile(Graph &graph);

/**
 *  @brief  Функция, получающая граф из файла ввода.
 *  @param  graph  Текущий граф.
*/

void GetFileGraphInput(Graph &graph);

/**
 *  @brief  Функция, производящая попытку получения BMFO представления от пользователя.
 *  @param  number_of_colums  Количество строк для текущего пердставления в файле.
 *  @param  bmfo_me  ME-вектор.
 *  @param  bmfo_mv  MV-вектор.
 *  @return Возвращает значение true - если завершено без ошибки и false, если исполнение
 *  произошло с ошибкой.
*/

bool TryGetInfoBMFOFromFile(int32_t number_of_colums, std::vector<int> &bmfo_me, std::vector<int>
        &bmfo_mv);

/**
 *  @brief  Функция, производящая попытку получения MFI представления от пользователя.
 *  @param  number_of_colums  Количество строк для текущего пердставления в файле.
 *  @param  mfi_me  ME-вектор.
 *  @param  mfi_mv  MV-вектор.
 *  @return Возвращает значение true - если завершено без ошибки и false, если исполнение
 *  произошло с ошибкой.
*/

bool TryGetMFIFromFile(int32_t number_of_colums, std::vector<int> &mfi_me, std::vector<int> &mfi_mv);

/**
 *  @brief  Функция, производящая попытку получения MFO представления от пользователя.
 *  @param  number_of_colums  Количество строк для текущего пердставления в файле.
 *  @param  mfo_me  ME-вектор.
 *  @param  mfo_mv  MV-вектор.
 *  @return Возвращает значение true - если завершено без ошибки и false, если исполнение
 *  произошло с ошибкой.
*/

bool TryGetMFOFromFile(int32_t number_of_colums, std::vector<int> &mfo_me, std::vector<int> &mfo_mv);

/**
 *  @brief  Функция, производящая получения строки матрицы смежности из строки матрицы
 *  инциндентности.
 *  @param  adjacency_matrix  Матрица смежности.
 *  @param  i  Строка матрицы инциндентности.
 *  @return Возвращает значение true - если завершено без ошибки и false, если исполнение
 *  произошло с ошибкой.
*/

bool GetStringOfAdjacencyMatrixFromIncidence(std::vector<std::vector<int>> &adjacency_matrix,
                                             std::vector<int> &i);
