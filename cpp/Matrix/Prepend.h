#pragma once

#include <vector>
#include <algorithm>

struct Prepend {
    template<typename T>
    const std::vector<std::vector<T>> operator()(std::vector<std::vector<T>> source, T value) const
    {
        for (std::vector<T> row : source) {
            row.insert(row.begin(), value);
        }

        return source;
    }
};

static const Prepend prepend = {};