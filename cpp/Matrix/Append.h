#pragma once

#include <vector>
#include <algorithm>

struct Append {
    template<typename T>
    const std::vector<std::vector<T>> operator()(std::vector<std::vector<T>> source, T value) const
    {
        for (std::vector<T> row : source) {
            row.push_back(value);
        }

        return source;
    }
};

static const Append append = {};