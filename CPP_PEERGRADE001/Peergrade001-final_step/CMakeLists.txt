cmake_minimum_required(VERSION 3.21)
project(Peergrade001)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_EXE_LINKER_FLAGS "-static")

add_executable(Peergrade001 main.cpp)
