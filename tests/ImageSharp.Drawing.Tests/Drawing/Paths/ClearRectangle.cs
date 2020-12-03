// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Drawing.Processing.Processors.Drawing;
using SixLabors.ImageSharp.Drawing.Tests.Processing;
using Xunit;

namespace SixLabors.ImageSharp.Drawing.Tests.Drawing.Paths
{
    public class ClearRectangle : BaseImageOperationsExtensionTest
    {
        private readonly IBrush brush = Brushes.Solid(Color.HotPink);
        private RectangleF rectangle = new RectangleF(10, 10, 20, 20);

        private RectangularPolygon RectanglePolygon => new RectangularPolygon(this.rectangle);

        [Fact]
        public void Brush()
        {
            this.operations.Clear(new ShapeGraphicsOptions(), this.brush, this.rectangle);

            FillPathProcessor processor = this.Verify<FillPathProcessor>();

            Assert.NotEqual(this.shapeOptions, processor.Options.ShapeOptions);
            Assert.Equal(this.RectanglePolygon, processor.Shape);
            Assert.Equal(this.brush, processor.Brush);
        }

        [Fact]
        public void BrushDefaultOptions()
        {
            this.operations.Clear(this.brush, this.rectangle);

            FillPathProcessor processor = this.Verify<FillPathProcessor>();

            Assert.Equal(this.shapeOptions, processor.Options.ShapeOptions);
            Assert.Equal(this.RectanglePolygon, processor.Shape);
            Assert.Equal(this.brush, processor.Brush);
        }

        [Fact]
        public void ColorSet()
        {
            this.operations.Clear(new ShapeGraphicsOptions(), Color.Red, this.rectangle);

            FillPathProcessor processor = this.Verify<FillPathProcessor>();

            Assert.NotEqual(this.shapeOptions, processor.Options.ShapeOptions);
            Assert.Equal(this.RectanglePolygon, processor.Shape);
            Assert.NotEqual(this.brush, processor.Brush);
            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(Color.Red, brush.Color);
        }

        [Fact]
        public void ColorAndThicknessDefaultOptions()
        {
            this.operations.Clear(Color.Red, this.rectangle);

            FillPathProcessor processor = this.Verify<FillPathProcessor>();

            Assert.Equal(this.shapeOptions, processor.Options.ShapeOptions);
            Assert.Equal(this.RectanglePolygon, processor.Shape);
            Assert.NotEqual(this.brush, processor.Brush);
            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(Color.Red, brush.Color);
        }
    }
}
