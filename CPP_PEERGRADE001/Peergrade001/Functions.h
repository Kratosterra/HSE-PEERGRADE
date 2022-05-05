#include <vector>

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
 *  @brief  Функция, которая обходит граф в ширину.
 *  @param  graph  Текущий граф.
*/

void BFS(const Graph& graph);

bool IsOriented(std::vector<std::vector<int>> matrix);
bool IsMulti(const std::vector<std::vector<int>> &matrix);
bool IsPseudo(const std::vector<std::vector<int>>& matrix, bool oriented);
int CountLoops(std::vector<std::vector<int>> matrix);
int CountArcs(const std::vector<std::vector<int>>& matrix);
int CountRibs(std::vector<std::vector<int>> matrix);
std::vector<std::vector<int>> GetVertexDegrees(std::vector<std::vector<int>> matrix,
                                               bool with_loops, bool oriented);
std::vector<std::vector<int>> ParseInIncidenceMatrix(const std::vector<std::vector<int>>& matrix,
                                                     int32_t arcs);
std::vector<std::vector<int>> ParseInAdjacencyList(const std::vector<std::vector<int>>& matrix);
std::vector<std::vector<int>> ParseInRibsList(const std::vector<std::vector<int>>& matrix,
                                              int32_t arcs);
std::vector<int> ParseInFO(const std::vector<std::vector<int>>& matrix, bool oriented);
std::vector<int> ParseInFI(const std::vector<std::vector<int>>& matrix, bool oriented);
std::pair<std::vector<int>, std::vector<int>> ParseInMFO(const std::vector<std::vector<int>>& matrix,
                                                         bool oriented);
std::pair<std::vector<int>, std::vector<int>> ParseInMFI(const std::vector<std::vector<int>>& matrix,
                                                         bool oriented);
std::vector<int> ParseInBFO(const std::vector<std::vector<int>>& matrix, bool oriented);
std::pair<std::vector<int>, std::vector<int>> ParseInBMFO(const std::vector<std::vector<int>>& matrix,
                                                          bool oriented);

std::vector<int> GetMatrixLine(int32_t size_of_matrix);
std::vector<std::string> Split(const std::string& s, char delim);
bool IsValidForMatrix(const std::vector<std::string>& line, std::vector<int>& ans);
bool IsValidString(const std::string& s);
std::string GetStringAdjacencyMatrix(const Graph &graph);
std::string GetStringIncidenceMatrix(const Graph &graph);
std::string GetStringAdjacencyList(const Graph &graph);
std::string GetStringRibsList(const Graph &graph);
std::string GetStringFO(const Graph &graph);
std::string GetStringFI(const Graph &graph);
std::string GetStringMFO(const Graph &graph);
std::string GetStringMFI(const Graph &graph);
std::string GetStringBFO(const Graph &graph);
void PrintVertexDegrees(const Graph &graph);
std::string GetStringBMFO(const Graph &graph);
void PrintMatrixRepresentation(const Graph &graph);
void PrintGraphMainInfo(const Graph &graph);
void SetAdjacencyMatrix(Graph &graph, const std::vector<std::vector<int>>& adjacency_matrix);
int32_t ParseNum(const std::string& s);
std::vector<std::string> SplitSegment(const std::string& sentence);
std::vector<std::vector<int>> ParseInIncidenceMatrix(
        const std::vector<std::vector<int>>& adjacency_list);