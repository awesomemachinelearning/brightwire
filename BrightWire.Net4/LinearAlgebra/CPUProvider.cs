﻿using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Single;
using System.Linq;
using BrightWire.Models;

namespace BrightWire.LinearAlgebra
{
    internal class CpuProvider : ILinearAlgebraProvider
    {
        readonly bool _isStochastic;

        public CpuProvider(bool stochastic = true)
        {
            _isStochastic = stochastic;
        }

        protected virtual void Dispose(bool disposing)
        {
            // nop
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IVector CreateVector(IEnumerable<float> data)
        {
            return new CpuVector(DenseVector.OfEnumerable(data));
        }

        public IVector CreateVector(int length, Func<int, float> init)
        {
            return new CpuVector(DenseVector.Create(length, init));
        }

        public IMatrix CreateMatrix(IReadOnlyList<IVector> vectorData)
        {
            var rows = vectorData.Select(r => r.AsIndexable()).ToList();
            var columns = rows.First().Count;
            return CreateMatrix(rows.Count, columns, (i, j) => rows[i][j]);
        }

        public IMatrix CreateMatrix(int rows, int columns, Func<int, int, float> init)
        {
            return new CpuMatrix(DenseMatrix.Create(rows, columns, init));
        }

        public I3DTensor CreateTensor(IReadOnlyList<IMatrix> data)
        {
            return new Cpu3DTensor(data);
        }

        public void PushLayer()
        {
            // nop
        }
        public void PopLayer()
        {
            // nop
        }

        public bool IsStochastic => _isStochastic;
        public bool IsGpu => false;
    }
}
