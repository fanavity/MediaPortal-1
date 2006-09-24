#region Copyright (C) 2006 Team MediaPortal

/* 
 *	Copyright (C) 2006 Team MediaPortal
 *	http://www.team-mediaportal.com
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */
#endregion

using System;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using MediaPortal.GUI.Library;
using MediaPortal.Dialogs;
using MediaPortal.Util;

namespace MediaPortal.GUI.NumberPlace
{
  public class GUINumberPlace : GUIWindow, ISetupForm, IShowPlugin
  {
    #region Serialization

    [Serializable]
    public class Settings
    {
      protected bool m_bShow;
      protected bool m_bBlock;
      protected int m_bLevel;

      public Settings()
      {
        m_bShow = false;
        m_bBlock = false;
        m_bLevel = (int)LevelName.Easy;
      }

      [XmlElement("Show")]
      public bool Show
      {
        get { return m_bShow; }
        set { m_bShow = value; }
      }

      [XmlElement("Block")]
      public bool Block
      {
        get { return m_bBlock; }
        set { m_bBlock = value; }
      }

      [XmlElement("Level")]
      public int Level
      {
        get { return m_bLevel; }
        set { m_bLevel = value; }
      }

      public void Load()
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.Get(Config.Dir.Config) + "MediaPortal.xml"))
        {
          m_bShow = xmlreader.GetValueAsBool("NumberPlace", "showerrormoves", false);
          m_bBlock = xmlreader.GetValueAsBool("NumberPlace", "blockerrormoves", false);
          m_bLevel = xmlreader.GetValueAsInt("NumberPlace", "level", 1);
        }
      }

      public void Save()
      {
        using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.Get(Config.Dir.Config) + "MediaPortal.xml"))
        {
          xmlwriter.SetValueAsBool("NumberPlace", "showerrormoves", m_bShow);
          xmlwriter.SetValueAsBool("NumberPlace", "blockerrormoves", m_bBlock);
          xmlwriter.SetValue("NumberPlace", "level", m_bLevel);
        }
      }
    }

    #endregion Serialization

    enum LevelName
    {
      Kids = 0,
      Easy = 1,
      Medium = 2,
      Hard = 3
    }

    private Grid grid = new Grid(3);
    private static Random random = new Random(DateTime.Now.Millisecond);

    [SkinControlAttribute(6)]    protected GUIToggleButtonControl btnBlockInvalidMoves = null;
    [SkinControlAttribute(7)]    protected GUIToggleButtonControl btnShowInvalidMoves = null;
    [SkinControlAttribute(8)]    protected GUIButtonControl btnLevel = null;

    static private readonly string pluginConfigFileName = "mynumberplace";

    protected static long m_dwCellIncorrectTextColor = 0xFFFF0000;
    protected static long m_dwTextColor = 0xFFFFFFFF;

    Settings _Settings = new Settings();

    public GUINumberPlace()
    {
      GetID = (int)GUIWindow.Window.WINDOW_NUMBERPLACE;
    }

    #region ISetupForm Members
    public int GetWindowId()
    {
      return GetID;
    }

    public string PluginName()
    {
      return "My Sudoku";
    }

    public string Description()
    {
      return "Play a Sudoku during commercials!";
    }

    public string Author()
    {
      return "Cosmo/IMOON/rtv";
    }

    public bool CanEnable()
    {
      return true;
    }

    public bool DefaultEnabled()
    {
      return false;
    }

    public bool HasSetup()
    {
      return false;
    }
    #endregion


    #region IShowPlugin Members
    public bool GetHome(out string strButtonText, out string strButtonImage, out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = GUILocalizeStrings.Get(19101);
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = "hover_" + pluginConfigFileName + ".png";
      return true;
    }

    public void ShowPlugin()
    {
      MessageBox.Show("Nothing to setup.");
    }

    public bool ShowDefaultHome()
    {
      return false;
    }
    #endregion


    public override bool Init()
    {
      // pre-register the control class so that the factory knows how to create it
      GUIControlFactory.RegisterControl("cell", typeof(CellControl));

      // Create our skin xml file
      CreateSkinXML(GetWindowId(), GUIGraphicsContext.Skin);

      // Load the skin xml file
      return Load(GUIGraphicsContext.Skin + @"\" + pluginConfigFileName + ".xml");
    }

    /// <summary>
    /// Create the skin xml files
    /// </summary>
    /// <param name="PluginID">the ID of the plugin</param>
    /// <param name="SkinDirectory">the directory where the skin xml files live</param>
    private void CreateSkinXML(int PluginID, string SkinDirectory)
    {
      Assembly asm = Assembly.GetExecutingAssembly();
      string[] resourceNames = asm.GetManifestResourceNames();
      foreach (string resource in resourceNames)
      {
        // If the embedded resource is a skin file
        if (resource.IndexOf("skin") > 0)
        {
          // Extract the extension of the resource
          int lastDot = resource.LastIndexOf(".");
          string extension = resource.Substring(lastDot);

          // Now replace all the '.'s with '\'s
          string name = resource.Remove(lastDot, resource.Length - lastDot);
          name = name.Replace("numberplace_mp.skin", "");
          name = name.Replace(".", "" + Path.DirectorySeparatorChar);

          // Finally put together the appropriate name for the skin file
          string fileName = SkinDirectory + name + extension;

          if (!File.Exists(fileName))
          {
            // Delete the skin file if it already exists - not
            //File.Delete(fileName);					

            // Now create the skin file only if it not exists
            Stream writer = File.Create(fileName);

            // Copy the contents of the embedded resource into the skin file
            Stream reader = asm.GetManifestResourceStream(resource);

            int data = reader.ReadByte();
            while (data != -1)
            {
              writer.WriteByte((byte)data);
              data = reader.ReadByte();
            }
            writer.Flush();
            writer.Close();
            reader.Close();
          }
        }
      }
    }

    protected override void OnPageLoad()
    {
      try
      {
        _Settings.Load();
        ShowInvalid();

        if (_Settings.Show)
        {
          GUIControl.SelectControl(GetID, 7);
        }
        else
        {
          GUIControl.DeSelectControl(GetID, 7);
        }

        if (_Settings.Block)
        {
          GUIControl.SelectControl(GetID, 6);
        }
        else
        {
          GUIControl.DeSelectControl(GetID, 6);
        }
        UpdateButtonStates();
      }
      catch (Exception e1)
      {
        Log.Error("GUINumberPlace: Exception occured - {0}", e1.Message);
      }

    }

    private void ClearGrid()
    {
      grid.Reset();

      for (int row = 0; row < grid.CellsInRow; row++)
      {
        for (int column = 0; column < grid.CellsInRow; column++)
        {
          int cellControlId = (1000 * (row + 1)) + column;
          CellControl cntlFoc = (CellControl)GetControl(cellControlId);
          cntlFoc.editable = true;
          cntlFoc.CellValue = 0;
          cntlFoc.SolutionValue = 0;
        }
      }
    }

    private bool GridIsComplete()
    {
      for (int row = 0; row < grid.CellsInRow; row++)
      {
        for (int column = 0; column < grid.CellsInRow; column++)
        {
          int cellControlId = (1000 * (row + 1)) + column;
          CellControl cntlFoc = (CellControl)GetControl(cellControlId);
          if (cntlFoc.CellValue == 0)
            return false;
        }
      }
      return true;
    }

    private void ShowInvalid()
    {
      if (_Settings.Show)
        CellControl.M_dwCellIncorrectTextColor = m_dwCellIncorrectTextColor;
      else
        CellControl.M_dwCellIncorrectTextColor = m_dwTextColor;
    }

    private bool SolvedCorrect()
    {
      for (int row = 0; row < grid.CellsInRow; row++)
      {
        for (int column = 0; column < grid.CellsInRow; column++)
        {
          int cellControlId = (1000 * (row + 1)) + column;
          CellControl cntlFoc = (CellControl)GetControl(cellControlId);
          if ((cntlFoc.CellValue != cntlFoc.SolutionValue) && cntlFoc.editable)
          {
            return false;
          }
        }
      }
      return true;
    }

    private Grid GenerateLevel(Grid puzzle, int minrating, int maxrating)
    {
      int i = 0, rating = 0;

      while (i < 500)
      {
        i++;
        puzzle = Solver.Generate(3);
        rating = Solver.Rate(puzzle);
        if (rating > minrating && rating < maxrating)
        {
          break;
        }
        if (i == 499)
        {
          Log.Debug("GUINumberPlace: None of the generated games wherre hard enough - aborting with new game at rating: {0}", rating);
          break;
        }
      }
      Log.Info("GUINumberPlace: New game generated after {1} loops, rating: {0}", rating, i);
      return puzzle;
    }

    private void UpdateButtonStates()
    {
      string textLine = GUILocalizeStrings.Get(19107);
      switch ((LevelName)_Settings.Level)
      {
        case LevelName.Kids:
          textLine += GUILocalizeStrings.Get(19115); // kids
          break;
        case LevelName.Easy:
          textLine += GUILocalizeStrings.Get(19108); // easy
          break;
        case LevelName.Medium:
          textLine += GUILocalizeStrings.Get(19109); // medium
          break;
        case LevelName.Hard:
          textLine += GUILocalizeStrings.Get(19110); // difficult
          break;
      }
      GUIControl.SetControlLabel(GetID, btnLevel.GetID, textLine);
    }

    private void Result()
    {
      GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
      if (SolvedCorrect())
      {
        dlg.SetHeading(GUILocalizeStrings.Get(19111)); // Game Over
        dlg.SetLine(1, GUILocalizeStrings.Get(19112)); // Congratulation!
        dlg.SetLine(2, GUILocalizeStrings.Get(19113)); // You have solved the game correctly.
        dlg.SetLine(3, String.Empty);
        //Utils.PlaySound("notify.wav", false, true);
      }
      else
      {
        dlg.SetHeading(GUILocalizeStrings.Get(19111)); // Game Over
        dlg.SetLine(1, GUILocalizeStrings.Get(19114)); // Sorry, but your solution is wrong.
        dlg.SetLine(2, String.Empty);
        dlg.SetLine(3, String.Empty);
      }
      dlg.DoModal(GUIWindowManager.ActiveWindow);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      if (controlId == 3)
      {
        // Solve grid
        Grid solution = Solver.Solve(grid);
        if (solution != null)
        {
          for (int row = 0; row < grid.CellsInRow; row++)
          {
            for (int column = 0; column < grid.CellsInRow; column++)
            {
              int cellControlId = (1000 * (row + 1)) + column;
              CellControl cntlFoc = (CellControl)GetControl(cellControlId);
              cntlFoc.CellValue = solution.cells[row, column];
              cntlFoc.editable = false;
            }
          }
        }

      }
      else if (controlId == 2)
      {
        //new game
        int minrating = 0;
        int maxrating = 0;
        ClearGrid();
        Grid puzzle = new Grid();
        switch ((LevelName)_Settings.Level)
        {
          case LevelName.Kids:
            minrating = 50;
            maxrating = 250;
            break;
          case LevelName.Easy:
            minrating = 150;
            maxrating = 350;
            break;
          case LevelName.Medium:
            minrating = 250;
            maxrating = 450;
            break;
          case LevelName.Hard:
            minrating = 450;
            maxrating = 999;
            break;
        }
        puzzle = GenerateLevel(puzzle, minrating, maxrating);

        //puzzle = Solver.Generate(3);
        Grid solution = Solver.Solve(puzzle);
        if ((LevelName)_Settings.Level == LevelName.Easy)
        {
          puzzle = Solver.FillOutCells(puzzle, solution, 10);
        }
        else if ((LevelName)_Settings.Level == LevelName.Kids)
        {
          puzzle = Solver.FillOutCells(puzzle, solution, 20);
        }

        for (int row = 0; row < grid.CellsInRow; row++)
        {
          for (int column = 0; column < grid.CellsInRow; column++)
          {
            int cellControlId = (1000 * (row + 1)) + column;
            CellControl cntlFoc = (CellControl)GetControl(cellControlId);
            cntlFoc.CellValue = puzzle.cells[row, column];
            if (cntlFoc.CellValue > 0)
            {
              cntlFoc.editable = false;
            }
            else
            {
              cntlFoc.SolutionValue = solution.cells[row, column];
            }
          }
        }
        grid = puzzle;
      }
      else if (control == btnBlockInvalidMoves)
      {
        _Settings.Block = btnBlockInvalidMoves.Selected;
        if (btnBlockInvalidMoves.Selected)
          if (btnShowInvalidMoves.Selected)
            _Settings.Show = btnShowInvalidMoves.Selected = false;
        _Settings.Save();
      }
      else if (controlId == 4)
      {
        ClearGrid();
      }
      else if (control == btnShowInvalidMoves)
      {
        _Settings.Show = btnShowInvalidMoves.Selected;
        if (btnShowInvalidMoves.Selected)
          if (btnBlockInvalidMoves.Selected)
            _Settings.Block = btnBlockInvalidMoves.Selected = false;
        ShowInvalid();
        _Settings.Save();
      }
      else if (control == btnLevel)
      {
        switch ((LevelName)_Settings.Level)
        {
          case LevelName.Kids:
            _Settings.Level = (int)LevelName.Easy;
            break;
          case LevelName.Easy:
            _Settings.Level = (int)LevelName.Medium;
            break;
          case LevelName.Medium:
            _Settings.Level = (int)LevelName.Hard;
            break;
          case LevelName.Hard:
            _Settings.Level = (int)LevelName.Kids;
            break;
        }
        UpdateButtonStates();
        _Settings.Save();
      }
      else if (controlId == 9)
      {
        int candidateIndex = random.Next(81 - grid.CountFilledCells());
        int m = -1, row = 0, column = 0;
        for (row = 0; row < 9 && m < candidateIndex; row++)
        {
          for (column = 0; column < 9 && m < candidateIndex; column++)
          {
            int cellControlId = (1000 * (row + 1)) + column;
            CellControl cntlFoc = (CellControl)GetControl(cellControlId);
            if (cntlFoc.editable == true && cntlFoc.CellValue == 0)
            {
              m++;
              if (m == candidateIndex)
              {
                cntlFoc.CellValue = cntlFoc.SolutionValue;
                grid.cells[row, column] = cntlFoc.SolutionValue;

              }
            }
          }
        }

      }
      base.OnClicked(controlId, control, actionType);
    }

    public override void OnAction(Action action)
    {

      if (action.wID == Action.ActionType.ACTION_SELECT_ITEM || action.wID == Action.ActionType.ACTION_MOUSE_CLICK)
      {
        int controlId = GetFocusControlId();
        if (controlId >= 1000 && controlId <= 9008)
        {
          // Show dialog
          CellControl cntlFoc = (CellControl)GetControl(controlId);
          int row = (controlId / 1000) - 1;
          int column = controlId % 1000;

          if (cntlFoc.editable)
          {

            GUIDialogMenu dlg = (GUIDialogMenu)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
            if (dlg != null)
            {
              dlg.Reset();
              dlg.SetHeading(GUILocalizeStrings.Get(19116));

              for (int index = 1; index < 10; index++)
              {
                dlg.Add("");
              }
              dlg.Add(GUILocalizeStrings.Get(19117));
              dlg.SelectedLabel = cntlFoc.CellValue - 1;
              dlg.DoModal(GetWindowId());
              if (dlg.SelectedLabel < 0)
              {
                return;
              }
              else
              {
                if (dlg.SelectedId == 10)
                {
                  cntlFoc.CellValue = 0;
                  grid.cells[row, column] = 0;
                }
                else
                {
                  if (!_Settings.Block || cntlFoc.SolutionValue == dlg.SelectedId)
                  {
                    cntlFoc.CellValue = dlg.SelectedId;
                    grid.cells[row, column] = dlg.SelectedId;
                    if (this.GridIsComplete())
                      this.Result();
                  }
                }
              }
            }
          }

        }
      }
      else

        if (action.wID == Action.ActionType.ACTION_KEY_PRESSED ||
          (action.wID >= Action.ActionType.REMOTE_0 && action.wID <= Action.ActionType.REMOTE_9))
        {
          int controlId = GetFocusControlId();
          if (controlId >= 1000 && controlId <= 9008)
          {
            CellControl cntlFoc = (CellControl)GetControl(controlId);
            int row = (controlId / 1000) - 1;
            int column = controlId % 1000;

            if (cntlFoc != null)
            {
              if (action.wID == Action.ActionType.ACTION_KEY_PRESSED)
              {
                if (action.m_key.KeyChar >= 49 && action.m_key.KeyChar <= 57)
                {
                  int value = action.m_key.KeyChar - 48;

                  if (!_Settings.Block || cntlFoc.SolutionValue == value)
                  {
                    cntlFoc.CellValue = value;
                    grid.cells[row, column] = value;
                    if (this.GridIsComplete())
                      this.Result();
                  }
                }
                else if (action.m_key.KeyChar == 8)
                {
                  cntlFoc.CellValue = 0;
                  grid.cells[row, column] = 0;
                }
              }
              else if (action.wID >= Action.ActionType.REMOTE_0 && action.wID <= Action.ActionType.REMOTE_9)
              {
                int value = (action.wID - Action.ActionType.REMOTE_0);

                if (value == 0 || !_Settings.Block || cntlFoc.SolutionValue == value)
                {
                  cntlFoc.CellValue = value;
                  grid.cells[row, column] = value;
                  if (this.GridIsComplete())
                  {

                  }
                  //this.Result();
                }
              }
            }
          }
        }

      base.OnAction(action);
    }
  }
}