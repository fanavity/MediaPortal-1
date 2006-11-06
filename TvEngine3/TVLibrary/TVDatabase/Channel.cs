//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the Gentle.NET Business Entity template, $Rev: 965 $
//========================================================================
using System;
using System.Collections;
using Gentle.Common;
using Gentle.Framework;
using System.Globalization;

namespace TvDatabase
{
  /// <summary>
  /// Instances of this class represent the properties and methods of a row in the table <b>Channel</b>.
  /// </summary>
  [TableName("Channel")]
  public class Channel : Persistent
  {
    Program _currentProgram = null;
    Program _nextProgram = null;
    #region Members
    private bool isChanged;
    [TableColumn("idChannel", NotNull = true), PrimaryKey(AutoGenerated = true)]
    private int idChannel;
    [TableColumn("name", NotNull = true)]
    private string name;
    [TableColumn("isRadio", NotNull = true)]
    private bool isRadio;
    [TableColumn("isTv", NotNull = true)]
    private bool isTv;
    [TableColumn("timesWatched", NotNull = true)]
    private int timesWatched;
    [TableColumn("totalTimeWatched", NotNull = true)]
    private DateTime totalTimeWatched;
    [TableColumn("grabEpg", NotNull = true)]
    private bool grabEpg;
    [TableColumn("lastGrabTime", NotNull = true)]
    private DateTime lastGrabTime;
    [TableColumn("sortOrder", NotNull = true)]
    private int sortOrder;
    [TableColumn("visibleInGuide", NotNull = true)]
    private bool visibleInGuide;
    [TableColumn("externalId", NotNull = true)]
    private string externalId;
    #endregion

    #region Constructors
    /// <summary> 
    /// Create a new object by specifying all fields (except the auto-generated primary key field). 
    /// </summary> 
    public Channel(string name, bool isRadio, bool isTv, int timesWatched, DateTime totalTimeWatched, bool grabEpg, DateTime lastGrabTime, int sortOrder, bool visibleInGuide, string externalId)
    {
      isChanged = true;
      this.name = name;
      this.isRadio = isRadio;
      this.isTv = isTv;
      this.timesWatched = timesWatched;
      this.totalTimeWatched = totalTimeWatched;
      this.grabEpg = grabEpg;
      this.lastGrabTime = lastGrabTime;
      this.sortOrder = sortOrder;
      this.visibleInGuide = visibleInGuide;
      this.externalId = externalId;
    }

    /// <summary> 
    /// Create an object from an existing row of data. This will be used by Gentle to 
    /// construct objects from retrieved rows. 
    /// </summary> 
    public Channel(int idChannel, string name, bool isRadio, bool isTv, int timesWatched, DateTime totalTimeWatched, bool grabEpg, DateTime lastGrabTime, int sortOrder, bool visibleInGuide, string externalId)
    {
      this.idChannel = idChannel;
      this.name = name;
      this.isRadio = isRadio;
      this.isTv = isTv;
      this.timesWatched = timesWatched;
      this.totalTimeWatched = totalTimeWatched;
      this.grabEpg = grabEpg;
      this.lastGrabTime = lastGrabTime;
      this.sortOrder = sortOrder;
      this.visibleInGuide = visibleInGuide;
      this.externalId = externalId;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Indicates whether the entity is changed and requires saving or not.
    /// </summary>
    public bool IsChanged
    {
      get { return isChanged; }
    }

    /// <summary>
    /// Property relating to database column idChannel
    /// </summary>
    public int IdChannel
    {
      get { return idChannel; }
    }

    /// <summary>
    /// Property relating to database column name
    /// </summary>
    public string Name
    {
      get { return name; }
      set { isChanged |= name != value; name = value; }
    }

    /// <summary>
    /// Property relating to database column name
    /// </summary>
    public string ExternalId
    {
      get { return externalId; }
      set { isChanged |= externalId != value; externalId = value; }
    }
    /// <summary>
    /// Property relating to database column isRadio
    /// </summary>
    public bool IsRadio
    {
      get { return isRadio; }
      set { isChanged |= isRadio != value; isRadio = value; }
    }

    /// <summary>
    /// Property relating to database column isTv
    /// </summary>
    public bool IsTv
    {
      get { return isTv; }
      set { isChanged |= isTv != value; isTv = value; }
    }

    /// <summary>
    /// Property relating to database column timesWatched
    /// </summary>
    public int TimesWatched
    {
      get { return timesWatched; }
      set { isChanged |= timesWatched != value; timesWatched = value; }
    }

    /// <summary>
    /// Property relating to database column totalTimeWatched
    /// </summary>
    public DateTime TotalTimeWatched
    {
      get { return totalTimeWatched; }
      set { isChanged |= totalTimeWatched != value; totalTimeWatched = value; }
    }

    /// <summary>
    /// Property relating to database column grabEpg
    /// </summary>
    public bool GrabEpg
    {
      get { return grabEpg; }
      set { isChanged |= grabEpg != value; grabEpg = value; }
    }

    /// <summary>
    /// Property relating to database column lastGrabTime
    /// </summary>
    public DateTime LastGrabTime
    {
      get { return lastGrabTime; }
      set { isChanged |= lastGrabTime != value; lastGrabTime = value; }
    }

    /// <summary>
    /// Property relating to database column sortOrder
    /// </summary>
    public int SortOrder
    {
      get { return sortOrder; }
      set { isChanged |= sortOrder != value; sortOrder = value; }
    }

    /// <summary>
    /// Property relating to database column visibleInGuide
    /// </summary>
    public bool VisibleInGuide
    {
      get { return visibleInGuide; }
      set { isChanged |= visibleInGuide != value; visibleInGuide = value; }
    }
    #endregion

    #region Storage and Retrieval

    /// <summary>
    /// Static method to retrieve all instances that are stored in the database in one call
    /// </summary>
    public static IList ListAll()
    {
      return Broker.RetrieveList(typeof(Channel));
    }

    /// <summary>
    /// Retrieves an entity given it's id.
    /// </summary>
    public static Channel Retrieve(int id)
    {
      // Return null if id is smaller than seed and/or increment for autokey
      if (id < 1)
      {
        return null;
      }
      Key key = new Key(typeof(Channel), true, "idChannel", id);
      return Broker.RetrieveInstance(typeof(Channel), key) as Channel;
    }

    /// <summary>
    /// Retrieves an entity given it's id, using Gentle.Framework.Key class.
    /// This allows retrieval based on multi-column keys.
    /// </summary>
    public static Channel Retrieve(Key key)
    {
      return Broker.RetrieveInstance(typeof(Channel), key) as Channel;
    }

    /// <summary>
    /// Persists the entity if it was never persisted or was changed.
    /// </summary>
    public override void Persist()
    {
      if (IsChanged || !IsPersisted)
      {
        base.Persist();
        isChanged = false;
      }
    }

    #endregion


    #region Relations

    /// <summary>
    /// Get a list of ChannelMap referring to the current entity.
    /// </summary>
    public IList ReferringChannelMap()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(ChannelMap));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(ChannelMap), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(ChannelMap), this );
    }

    /// <summary>
    /// Get a list of GroupMap referring to the current entity.
    /// </summary>
    public IList ReferringGroupMap()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(GroupMap));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(GroupMap), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(GroupMap), this );
    }
    /// <summary>
    /// Get a list of Conflicts referring to the current entity.
    /// </summary>
    public IList ReferringConflicts()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Conflict));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(Conflict), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(GroupMap), this );
    }
    
    /// <summary>
    /// Get a list of Program referring to the current entity.
    /// </summary>
    public IList ReferringProgram()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Program));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(Program), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(Program), this );
    }

    /// <summary>
    /// Get a list of Recording referring to the current entity.
    /// </summary>
    public IList ReferringRecording()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Recording));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(Recording), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(Recording), this );
    }

    /// <summary>
    /// Get a list of Schedule referring to the current entity.
    /// </summary>
    public IList ReferringSchedule()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Schedule));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(Schedule), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(Schedule), this );
    }

    /// <summary>
    /// Get a list of TuningDetail referring to the current entity.
    /// </summary>
    public IList ReferringTuningDetail()
    {
      //select * from 'foreigntable'
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(TuningDetail));

      // where foreigntable.foreignkey = ourprimarykey
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);

      // passing true indicates that we'd like a list of elements, i.e. that no primary key
      // constraints from the type being retrieved should be added to the statement
      SqlStatement stmt = sb.GetStatement(true);

      // execute the statement/query and create a collection of User instances from the result set
      return ObjectFactory.GetCollection(typeof(TuningDetail), stmt.Execute());

      // TODO In the end, a GentleList should be returned instead of an arraylist
      //return new GentleList( typeof(TuningDetail), this );
    }
    #endregion

    public Program NextProgram
    {
      get
      {
        UpdateNowAndNext();
        return _nextProgram;
      }
    }
    public Program CurrentProgram
    {
      get
      {
        UpdateNowAndNext();
        return _currentProgram;
      }
    }

    public Program GetProgramAt(DateTime date)
    {
      IFormatProvider mmddFormat = new CultureInfo(String.Empty, false);
      DateTime startTime = DateTime.Now;
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Program));
      sb.AddConstraint(Operator.Equals, "idChannel", IdChannel);
      sb.AddConstraint(String.Format("endTime >= '{0}'", date.ToString("yyyyMMdd HH:mm:ss", mmddFormat)));
      sb.AddOrderByField(true, "starttime");
      sb.SetRowLimit(1);
      SqlStatement stmt = sb.GetStatement(true);
      IList programs = ObjectFactory.GetCollection(typeof(Program), stmt.Execute());
      if (programs.Count == 0)
      {
        return null;
      }
      return (Program)programs[0];
    }
    void UpdateNowAndNext()
    {
      if (_currentProgram != null)
      {
        if (DateTime.Now >= _currentProgram.StartTime && DateTime.Now <= _currentProgram.EndTime)
        {
          return;
        }
      }

      _currentProgram = null;
      _nextProgram = null;

      IFormatProvider mmddFormat = new CultureInfo(String.Empty, false);
      DateTime date = DateTime.Now;
      SqlBuilder sb = new SqlBuilder(StatementType.Select, typeof(Program));
      sb.AddConstraint(Operator.Equals, "idChannel", IdChannel);
      sb.AddConstraint(String.Format("endTime >= '{0}'", date.ToString("yyyyMMdd HH:mm:ss", mmddFormat)));
      sb.AddOrderByField(true, "starttime");
      sb.SetRowLimit(2);
      SqlStatement stmt = sb.GetStatement(true);
      IList programs = ObjectFactory.GetCollection(typeof(Program), stmt.Execute());
      if (programs.Count == 0)
      {
        return;
      }
      _currentProgram = (Program)programs[0];
      if (programs.Count == 2)
      {
        _nextProgram = (Program)programs[1];
      }
    }

    public void Delete()
    {
      IList list = ReferringConflicts();
      foreach (Conflict conflict in list)
        conflict.Remove();

      list = ReferringGroupMap();
      foreach (GroupMap map in list)
        map.Remove();

      list = ReferringProgram();
      foreach (Program program in list)
        program.Delete();

      list = ReferringSchedule();
      foreach (Schedule schedule in list)
        schedule.Delete();

      list = ReferringRecording();
      foreach (Recording rec in list)
        rec.Remove();

      list = ReferringChannelMap();
      foreach (ChannelMap channelMap in list)
        channelMap.Remove();

      list = ReferringTuningDetail();
      foreach (TuningDetail detail in list)
        detail.Remove();
      Remove();
    }
  }
}
