using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Components
{
    public class GridBounds
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
    }

    public class GridModifier
    {
        private Grid grid;
        public CheckBox[,] CheckBoxGrid { get; private set; }

        public GridModifier(Grid grid)
        {
            this.grid = grid;
            AddPlaceholdersToGrid(grid);
            AddSplittersToGrid(grid);
        }

        private void AddSplittersToGrid(Grid grid)
        {
            Dictionary<RowDefinition, int> rowDefinitions = GetAutoRows(grid);
            Dictionary<ColumnDefinition, int> columnDefinitions = GetAutoColumns(grid);

            foreach (KeyValuePair<RowDefinition, int> rowDefinitionPair in rowDefinitions)
            {
                GridSplitter gridSplitter = new GridSplitter();
                gridSplitter.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                gridSplitter.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                Grid.SetRow(gridSplitter, rowDefinitionPair.Value);
                Grid.SetColumnSpan(gridSplitter, grid.ColumnDefinitions.Count);
                gridSplitter.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                gridSplitter.ResizeDirection = GridResizeDirection.Rows;
                gridSplitter.Height = 5;
                gridSplitter.Background = (Brush)new BrushConverter().ConvertFromString("#FFBCBCBC");

                grid.Children.Add(gridSplitter);
            }

            foreach (KeyValuePair<ColumnDefinition, int> columnDefinitionPair in columnDefinitions)
            {
                GridSplitter gridSplitter = new GridSplitter();
                gridSplitter.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                gridSplitter.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                Grid.SetColumn(gridSplitter, columnDefinitionPair.Value);
                Grid.SetRowSpan(gridSplitter, grid.RowDefinitions.Count);
                gridSplitter.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
                gridSplitter.ResizeDirection = GridResizeDirection.Columns;
                gridSplitter.Width = 5;
                gridSplitter.Background = (Brush)new BrushConverter().ConvertFromString("#FFBCBCBC");

                grid.Children.Add(gridSplitter);
            }
        }

        private void AddPlaceholdersToGrid(Grid grid)
        {
            Dictionary<RowDefinition, int> rowDefinitions = GetNonAutoRows(grid);
            Dictionary<ColumnDefinition, int> columnDefinitions = GetNonAutoColumns(grid);

            CheckBoxGrid = new CheckBox[rowDefinitions.Count, columnDefinitions.Count];

            int i = 0, j = 0;
            foreach (KeyValuePair<RowDefinition, int> rowDefinitionPair in rowDefinitions)
            {
                foreach (KeyValuePair<ColumnDefinition, int> columnDefinitionPair in columnDefinitions)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.VerticalAlignment = VerticalAlignment.Center;
                    checkBox.HorizontalAlignment = HorizontalAlignment.Center;
                    ScaleTransform scaleTransform = new ScaleTransform(3.0, 3.0);
                    checkBox.LayoutTransform = scaleTransform;

                    int row = rowDefinitionPair.Value;
                    int col = columnDefinitionPair.Value;

                    Grid.SetRow(checkBox, row);
                    Grid.SetColumn(checkBox, col);

                    CheckBoxGrid[i, j] = checkBox;
                    grid.Children.Add(checkBox);
                    j++;
                }
                j = 0;
                i++;
            }
        }

        public void RemoveComponent(UIElement element)
        {
            int firstRow = Grid.GetRow(element);
            int firstCol = Grid.GetColumn(element);

            int rowSpan = Grid.GetRowSpan(element);
            int colSpan = Grid.GetColumnSpan(element);

            int lastRow = firstRow + rowSpan - 1;
            int lastCol = firstCol + colSpan - 1;

            grid.Children.Remove(element);

            for (int i = 0; i < CheckBoxGrid.GetLength(0); i++)
            {
                for (int j = 0; j < CheckBoxGrid.GetLength(1); j++)
                {
                    CheckBox checkBox = CheckBoxGrid[i, j];
                    if (Grid.GetRow(checkBox) >= firstRow && Grid.GetRow(checkBox) <= lastRow && Grid.GetColumn(checkBox) >= firstCol && Grid.GetColumn(checkBox) <= lastCol)
                    {
                        checkBox.IsEnabled = true;
                        checkBox.IsChecked = false;
                    }
                }
            }
        }

        public bool AddComponentForCurrentSelection(UIElement element)
        {
            int? firstLocalRow = null, lastLocalRow = null, firstLocalCol = null, lastLocalCol = null;
            int? firstRow = null, lastRow = null, firstCol = null, lastCol = null;
            for (int i = 0; i < CheckBoxGrid.GetLength(0); i++)
            {
                for (int j = 0; j < CheckBoxGrid.GetLength(1); j++)
                {
                    if (CheckBoxGrid[i, j].IsChecked.Value && CheckBoxGrid[i, j].IsEnabled)
                    {
                        int checkBoxRow = Grid.GetRow(CheckBoxGrid[i, j]);
                        int checkBoxCol = Grid.GetColumn(CheckBoxGrid[i, j]);

                        if (firstRow.HasValue && lastRow.HasValue && firstCol.HasValue && lastCol.HasValue)
                        {
                            if (checkBoxRow < firstRow)
                            {
                                firstRow = checkBoxRow;
                            }
                            if (checkBoxRow > lastRow)
                            {
                                lastRow = checkBoxRow;
                            }
                            if (checkBoxCol < firstCol)
                            {
                                firstCol = checkBoxCol;
                            }
                            if (checkBoxCol > lastCol)
                            {
                                lastCol = checkBoxCol;
                            }
                        }
                        else
                        {
                            firstRow = lastRow = checkBoxRow;
                            firstCol = lastCol = checkBoxCol;
                        }

                        if (firstLocalRow.HasValue && lastLocalRow.HasValue && firstLocalCol.HasValue && lastLocalCol.HasValue)
                        {
                            if (i < firstLocalRow)
                            {
                                firstLocalRow = i;
                            }
                            if (i > lastLocalRow)
                            {
                                lastLocalRow = i;
                            }
                            if (j < firstLocalCol)
                            {
                                firstLocalCol = j;
                            }
                            if (j > lastLocalCol)
                            {
                                lastLocalCol = j;
                            }
                        }
                        else
                        {
                            firstLocalRow = lastLocalRow = i;
                            firstLocalCol = lastLocalCol = j;
                        }
                    }
                }
            }

            if (!firstRow.HasValue || !lastRow.HasValue || !firstCol.HasValue || !lastCol.HasValue)
            {
                return false;
            }

            bool[,] checkMap = new bool[CheckBoxGrid.GetLength(0), CheckBoxGrid.GetLength(1)];

            for (int i = 0; i < CheckBoxGrid.GetLength(0); i++)
            {
                for (int j = 0; j < CheckBoxGrid.GetLength(1); j++)
                {
                    if (i < firstLocalRow.Value || i > lastLocalRow.Value || j < firstLocalCol.Value || j > lastLocalCol.Value)
                    {
                        checkMap[i, j] = true;
                    }
                    else if (CheckBoxGrid[i, j].IsChecked.Value && CheckBoxGrid[i, j].IsEnabled)
                    {
                        checkMap[i, j] = true;
                    }
                }
            }

            for (int i = firstLocalRow.Value; i <= lastLocalRow.Value; i++)
            {
                for (int j = firstLocalCol.Value; j <= lastLocalCol.Value; j++)
                {
                    if (!checkMap[i, j])
                    {
                        return false;
                    }
                }
            }

            for (int i = firstLocalRow.Value; i <= lastLocalRow; i++)
            {
                for (int j = firstLocalCol.Value; j <= lastLocalCol.Value; j++)
                {
                    CheckBoxGrid[i, j].IsEnabled = false;
                }
            }

            Grid.SetRow(element, firstRow.Value);
            Grid.SetColumn(element, firstCol.Value);

            int rowSpan = lastRow.Value - firstRow.Value + 1;
            int colSpan = lastCol.Value - firstCol.Value + 1;

            Grid.SetRowSpan(element, rowSpan);
            Grid.SetColumnSpan(element, colSpan);

            grid.Children.Add(element);

            return true;
        }

        private Dictionary<RowDefinition, int> GetAutoRows(Grid grid)
        {
            Dictionary<RowDefinition, int> rowDefinitions = new Dictionary<RowDefinition, int>();
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                RowDefinition rowDefinition = grid.RowDefinitions[i];
                if (rowDefinition.Height.IsAuto)
                {
                    rowDefinitions[rowDefinition] = i;
                }
            }
            return rowDefinitions;
        }

        private Dictionary<RowDefinition, int> GetNonAutoRows(Grid grid)
        {
            Dictionary<RowDefinition, int> rowDefinitions = new Dictionary<RowDefinition, int>();
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                RowDefinition rowDefinition = grid.RowDefinitions[i];
                if (!rowDefinition.Height.IsAuto)
                {
                    rowDefinitions[rowDefinition] = i;
                }
            }
            return rowDefinitions;
        }

        private Dictionary<ColumnDefinition, int> GetAutoColumns(Grid grid)
        {
            Dictionary<ColumnDefinition, int> columnDefinitions = new Dictionary<ColumnDefinition, int>();
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                ColumnDefinition columnDefinition = grid.ColumnDefinitions[i];
                if (columnDefinition.Width.IsAuto)
                {
                    columnDefinitions[columnDefinition] = i;
                }
            }
            return columnDefinitions;
        }

        private Dictionary<ColumnDefinition, int> GetNonAutoColumns(Grid grid)
        {
            Dictionary<ColumnDefinition, int> columnDefinitions = new Dictionary<ColumnDefinition, int>();
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                ColumnDefinition columnDefinition = grid.ColumnDefinitions[i];
                if (!columnDefinition.Width.IsAuto)
                {
                    columnDefinitions[columnDefinition] = i;
                }
            }
            return columnDefinitions;
        }

    }
}
