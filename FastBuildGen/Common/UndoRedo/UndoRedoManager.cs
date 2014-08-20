using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    internal class UndoRedoManager : IUndoRedoManager
    {
        #region Members

        private readonly BrowsableList<IUndoRedoAction> _actions;

        private bool _isFatalExceptionInMacroBloc;
        private bool _isValidMacroBloc;
        private bool _lastCanRedo;
        private bool _lastCanUndo;
        private int _lastRedoActionsCount;
        private int? _lastRelativeTokenPosition;
        private int _lastUndoActionsCount;
        private Stack<ComposedUndoRedoAction> _macroActions;

        #endregion Members

        #region ctor

        public UndoRedoManager()
        {
            _actions = new BrowsableList<IUndoRedoAction>();
            ClearCore();
        }

        #endregion ctor

        #region Property

        public bool CanRedo
        {
            get { return _lastCanRedo; }
        }

        public bool CanUndo
        {
            get { return _lastCanUndo; }
        }

        /// <summary>
        /// First redoable to last redoable.
        /// </summary>
        public IEnumerable<IUndoRedoActionDescription> RedoActions
        {
            get { return _actions.Nexts; }
        }

        public int? RelativeTokenPosition
        {
            get { return _actions.RelativeTokenPosition; }
        }

        /// <summary>
        /// First undoable to last undoable.
        /// </summary>
        public IEnumerable<IUndoRedoActionDescription> UndoActions
        {
            get { return _actions.Backs; }
        }

        #endregion Property

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion Event

        #region IUndoRedoManager

        public void Cancel()
        {
            throw new UndoRedoActionCancelledException();
        }

        public void Cancel(string message)
        {
            throw new UndoRedoActionCancelledException(message);
        }

        public void Cancel(string message, Exception innerException)
        {
            throw new UndoRedoActionCancelledException(message, innerException);
        }

        public void Clear()
        {
            ClearCore();
        }

        public void Do(IUndoRedoAction action)
        {
            if (action == null)
                throw new FastBuildGenException("Null parameter not permitted for " + this.GetType().Name + ".Do(IUndoRedoAction)");

            if (IsInMacroBloc)
            {
                DoInMacroBloc(action);
            }
            else
            {
                DoCore(action);
            }
        }

        public void Do(string name, string title, Action doAction, Action undoAction)
        {
            Do(new UndoRedoAction(name, title, doAction, undoAction));
        }

        public void Do(string name, string title, Action doAction, Action redoAction, Action undoAction)
        {
            Do(new UndoRedoAction(name, title, doAction, redoAction, undoAction));
        }

        public UndoRedoActionMacroBloc NewUndoRedoActionMacroBloc(string name, string title)
        {
            return new UndoRedoActionMacroBloc(this, name, title);
        }

        public void Redo()
        {
            RedoCore();
        }

        public void ResetToken()
        {
            _actions.ResetToken();
            UpdateAndNotifyStatesCore();
        }

        public void SetToken()
        {
            _actions.SetToken();
            UpdateAndNotifyStatesCore();
        }

        public void StartMacroBloc(string name, string title)
        {
            StartMacroBlocCore(name, title);
        }

        public void StopMacroBloc(string name)
        {
            StopMacroBlocCore(name);
        }

        public void Undo()
        {
            UndoCore();
        }

        #endregion IUndoRedoManager

        #region Core

        private void ClearCore()
        {
            _actions.Clear();
            UpdateAndNotifyStatesCore();
        }

        private void DoCore(IUndoRedoAction action)
        {
            Debug.Assert(action != null);

            try
            {
                action.Do();
                // add action into the action list
                _actions.Push(action);
                UpdateAndNotifyStatesCore();
            }
            catch (UndoRedoActionCancelledException e)
            {
                // action cancelled
                throw e;    // re-throw in order to alerte the caller
            }
            catch (Exception e)
            {
                ClearCore();
                throw new UndoRedoUncatchedException(e);
            }
        }

        private void RedoCore()
        {
            if (false == _actions.CanMoveNext)
                return;

            try
            {
                IUndoRedoAction undoRedoAction = _actions.PeekNext();
                undoRedoAction.Redo();
                _actions.MoveNext();
                UpdateAndNotifyStatesCore();
            }
            catch (UndoRedoActionCancelledException e)
            {
                // stop as is, currents state of the manager don't change
                throw e;    // re-throw in order to alerte the caller
            }
            catch (Exception e)
            {
                ClearCore();
                throw new UndoRedoUncatchedException(e);
            }
        }

        private void UndoCore()
        {
            if (false == _actions.CanMoveBack)
                return;

            try
            {
                IUndoRedoAction undoRedoAction = _actions.PeekBack();
                undoRedoAction.Undo();
                _actions.MoveBack();
                UpdateAndNotifyStatesCore();
            }
            catch (UndoRedoActionCancelledException e)
            {
                // stop as is, currents state of the manager don't change
                throw e;    // re-throw in order to alerte the caller
            }
            catch (Exception e)
            {
                ClearCore();
                throw new UndoRedoUncatchedException(e);
            }
        }

        private void UpdateAndNotifyStatesCore()
        {
            bool canUndo = _actions.CanMoveBack;
            bool canRedo = _actions.CanMoveNext;
            int undoActionsCount = _actions.CursorPosition + 1; // _actions.Backs.Count();
            int redoActionsCount = _actions.Count - undoActionsCount; // _actions.Nexts.Count();
            int? relativeTokenPosition = _actions.RelativeTokenPosition;

            bool canUndoChanged = canUndo != _lastCanUndo;
            bool canRedoChanged = canRedo != _lastCanRedo;
            bool undoActionsCountChanged = undoActionsCount != _lastUndoActionsCount;
            bool redoActionsCountChanged = redoActionsCount != _lastRedoActionsCount;
            bool relativeTokenPositionChanged = relativeTokenPosition != _lastRelativeTokenPosition;

            _lastCanUndo = canUndo;
            _lastCanRedo = canRedo;
            _lastUndoActionsCount = undoActionsCount;
            _lastRedoActionsCount = redoActionsCount;
            _lastRelativeTokenPosition = relativeTokenPosition;

            if (canUndoChanged)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIUndoRedoManagerEvent.ConstCanUndo));
            if (canRedoChanged)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIUndoRedoManagerEvent.ConstCanRedo));
            if (undoActionsCountChanged)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIUndoRedoManagerEvent.ConstUndoActions));
            if (redoActionsCountChanged)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIUndoRedoManagerEvent.ConstRedoActions));
            if (relativeTokenPositionChanged)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIUndoRedoManagerEvent.ConstRelativeTokenPosition));
        }

        #region MacroBloc

        private bool IsInMacroBloc
        {
            get { return (_macroActions != null) && (_macroActions.Count > 0); }
        }

        private void DoInMacroBloc(IUndoRedoAction action)
        {
            Debug.Assert(action != null);

            try
            {
                action.Do();

                _macroActions.Peek().Add(action);
            }
            catch (UndoRedoActionCancelledException)
            {
                // action cancelled

                _isValidMacroBloc = false;
            }
            catch (Exception e)
            {
                _isValidMacroBloc = false;
                _isFatalExceptionInMacroBloc = true;

                throw new UndoRedoUncatchedException(e);
            }
        }

        private void StartMacroBlocCore(string name, string title)
        {
            if (false == IsInMacroBloc)
            {
                // start macro
                _isValidMacroBloc = true;
                _isFatalExceptionInMacroBloc = false;
                _macroActions = new Stack<ComposedUndoRedoAction>();
            } // else : start a macro in a macro (reentrancy)

            // add macro wich contains actions
            ComposedUndoRedoAction macroAction = new ComposedUndoRedoAction(name, title);
            _macroActions.Push(macroAction);
        }

        private void StopMacroBlocCore(string name)
        {
            if (false == IsInMacroBloc)
                throw new FastBuildGenException("Unexpected state");

            ComposedUndoRedoAction macroAction = _macroActions.Pop();
            IUndoRedoAction action = macroAction;

            // BEGIN : optim
            int macroActionCount = macroAction.Count();
            if (macroActionCount == 0)
            {
                // do nothing - empty macro
                action = null;
            }
            if (macroActionCount == 1)
            {
                IUndoRedoAction firstAction = macroAction.First();
                if ((macroAction.Name == firstAction.Name) && (macroAction.Title == firstAction.Title))
                    action = firstAction;// remove macro and preserved
            }
            // END : optim

            if (IsInMacroBloc)
            {
                // close one of the reentrancy
                _macroActions.Peek().Add(action);
            }
            else
            {
                // close macro bloc

                if (_isValidMacroBloc)
                {
                    // add action into the actions list
                    _actions.Push(action);
                    UpdateAndNotifyStatesCore();
                }
                else if (_isFatalExceptionInMacroBloc)
                {
                    // Undo/Redo no longer assumed
                    Clear();
                }
                _macroActions = null;
            }
        }

        #endregion MacroBloc

        #endregion Core

        #region Tests

#if DEBUG
        private const string ConstBaseNameA = "A";
        private const string ConstBaseNameB = "B";
        private const string ConstBaseNameBBis = "B'";
        private const string ConstBaseNameC = "C";
        private const string ConstBaseNameD = "D";
        private const string ConstBaseNameDBis = "D'";
        private const string ConstBaseNameDTer = "D\"";
        private const string ConstBaseNameE = "E";
        private const string ConstDoActionA = ConstPrefixDoAction + ConstBaseNameA;
        private const string ConstDoActionB = ConstPrefixDoAction + ConstBaseNameB;
        private const string ConstDoActionBBis = ConstPrefixDoAction + ConstBaseNameBBis;
        private const string ConstDoActionC = ConstPrefixDoAction + ConstBaseNameC;
        private const string ConstDoActionD = ConstPrefixDoAction + ConstBaseNameD;
        private const string ConstDoActionDBis = ConstPrefixDoAction + ConstBaseNameDBis;
        private const string ConstDoActionDTer = ConstPrefixDoAction + ConstBaseNameDTer;
        private const string ConstDoActionE = ConstPrefixDoAction + ConstBaseNameE;
        private const string ConstNameA = ConstPrefixName + ConstBaseNameA;
        private const string ConstNameB = ConstPrefixName + ConstBaseNameB;
        private const string ConstNameBBis = ConstPrefixName + ConstBaseNameBBis;
        private const string ConstNameC = ConstPrefixName + ConstBaseNameC;
        private const string ConstNameD = ConstPrefixName + ConstBaseNameD;
        private const string ConstNameDBis = ConstPrefixName + ConstBaseNameDBis;
        private const string ConstNameDTer = ConstPrefixName + ConstBaseNameDTer;
        private const string ConstNameE = ConstPrefixName + ConstBaseNameE;
        private const string ConstPrefixDoAction = "Do";
        private const string ConstPrefixName = "#";
        private const string ConstPrefixRedoAction = "Redo";
        private const string ConstPrefixTitle = "";
        private const string ConstPrefixUndoAction = "Undo";
        private const string ConstRedoActionA = ConstPrefixRedoAction + ConstBaseNameA;
        private const string ConstRedoActionB = ConstPrefixRedoAction + ConstBaseNameB;
        private const string ConstRedoActionBBis = ConstPrefixRedoAction + ConstBaseNameBBis;
        private const string ConstRedoActionC = ConstPrefixRedoAction + ConstBaseNameC;
        private const string ConstRedoActionD = ConstPrefixRedoAction + ConstBaseNameD;
        private const string ConstRedoActionDBis = ConstPrefixRedoAction + ConstBaseNameDBis;
        private const string ConstRedoActionDTer = ConstPrefixRedoAction + ConstBaseNameDTer;
        private const string ConstRedoActionE = ConstPrefixRedoAction + ConstBaseNameE;
        private const string ConstTitleA = ConstPrefixTitle + ConstBaseNameA;
        private const string ConstTitleB = ConstPrefixTitle + ConstBaseNameB;
        private const string ConstTitleBBis = ConstPrefixTitle + ConstBaseNameBBis;
        private const string ConstTitleC = ConstPrefixTitle + ConstBaseNameC;
        private const string ConstTitleD = ConstPrefixTitle + ConstBaseNameD;
        private const string ConstTitleDBis = ConstPrefixTitle + ConstBaseNameDBis;
        private const string ConstTitleDTer = ConstPrefixTitle + ConstBaseNameDTer;
        private const string ConstTitleE = ConstPrefixTitle + ConstBaseNameE;
        private const string ConstUndoActionA = ConstPrefixUndoAction + ConstBaseNameA;
        private const string ConstUndoActionB = ConstPrefixUndoAction + ConstBaseNameB;
        private const string ConstUndoActionBBis = ConstPrefixUndoAction + ConstBaseNameBBis;
        private const string ConstUndoActionC = ConstPrefixUndoAction + ConstBaseNameC;
        private const string ConstUndoActionD = ConstPrefixUndoAction + ConstBaseNameD;
        private const string ConstUndoActionDBis = ConstPrefixUndoAction + ConstBaseNameDBis;
        private const string ConstUndoActionDTer = ConstPrefixUndoAction + ConstBaseNameDTer;
        private const string ConstUndoActionE = ConstPrefixUndoAction + ConstBaseNameE;
        private static List<string> _listActions = new List<string>();

        [Conditional("DEBUG")]
        public static void Test()
        {
            const string separator = "##########################################################";

            Test0();
            Debug.WriteLine(separator);
            Test1();
            Debug.WriteLine(separator);
            Test2();
            Debug.WriteLine(separator);
            Test3();
            Debug.WriteLine(separator);
            Test4();
            Debug.WriteLine(separator);
            Test5();
            Debug.WriteLine(separator);
            Test6();
            Debug.WriteLine(separator);
            Test7();
        }

        [Conditional("DEBUG")]
        private static void Test0()
        {
            UndoRedoManager manager = new UndoRedoManager();
            _listActions.Clear();
            manager.Clear();
            List<string> listActions = new List<string>();

            listActions.Add(ConstDoActionA);
            manager.Do(TestActionA());

            listActions.Add(ConstDoActionB);
            manager.Do(TestActionB());

            listActions.Add(ConstDoActionC);
            manager.Do(TestActionC());

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionC);
            manager.Undo();

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionB);
            manager.Undo();

            Debug.WriteLine("Redo");
            listActions.Add(ConstRedoActionB);
            manager.Redo();

            Debug.WriteLine("Redo");
            listActions.Add(ConstRedoActionC);
            manager.Redo();

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionC);
            manager.Undo();

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionB);
            manager.Undo();

            listActions.Add(ConstDoActionD);
            manager.Do(TestActionD());

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionD);
            manager.Undo();

            listActions.Add(ConstUndoActionA);
            Debug.WriteLine("Undo");
            manager.Undo();

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        [Conditional("DEBUG")]
        private static void Test1()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            UndoRedoAction action = TestActionA();

            Debug.WriteLine("Do");
            listActions.Add(ConstDoActionA);
            manager.Do(action);

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionA);
            manager.Undo();

            Debug.WriteLine("Undo");
            // nothing
            manager.Undo();

            Debug.WriteLine("Redo");
            listActions.Add(ConstRedoActionA);
            manager.Redo();

            Debug.WriteLine("Redo");
            // nothing
            manager.Redo();

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        [Conditional("DEBUG")]
        private static void Test2()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            using (UndoRedoActionMacroBloc macroBloc = manager.NewUndoRedoActionMacroBloc(ConstNameA, ConstTitleA))
            {
                UndoRedoAction action = TestActionA();

                Debug.WriteLine("Do");
                listActions.Add(ConstDoActionA);
                macroBloc.Do(action);
            }

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionA);
            manager.Undo();

            Debug.WriteLine("Undo");
            // nothing
            manager.Undo();

            Debug.WriteLine("Redo");
            listActions.Add(ConstRedoActionA);
            manager.Redo();

            Debug.WriteLine("Redo");
            // nothing
            manager.Redo();

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        [Conditional("DEBUG")]
        private static void Test3()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            Debug.WriteLine("Do");
            using (UndoRedoActionMacroBloc macroBlocA = manager.NewUndoRedoActionMacroBloc(ConstNameA, ConstTitleA))
            {
                using (UndoRedoActionMacroBloc macroBlocB = manager.NewUndoRedoActionMacroBloc(ConstNameB, ConstTitleB))
                {
                    listActions.Add(ConstDoActionBBis);
                    macroBlocB.Do(TestActionBBis());
                    using (UndoRedoActionMacroBloc macroBlocC = manager.NewUndoRedoActionMacroBloc(ConstNameC, ConstTitleC))
                    {
                        using (UndoRedoActionMacroBloc macroBlocD = manager.NewUndoRedoActionMacroBloc(ConstNameD, ConstTitleD))
                        {
                            listActions.Add(ConstDoActionDBis);
                            macroBlocD.Do(TestActionDBis());
                            listActions.Add(ConstDoActionDTer);
                            macroBlocD.Do(TestActionDTer());

                            using (UndoRedoActionMacroBloc macroBlocE = manager.NewUndoRedoActionMacroBloc(ConstNameE, ConstTitleE))
                            {
                                listActions.Add(ConstDoActionE);
                                macroBlocE.Do(TestActionE());
                            }
                        }
                    }
                }
            }

            Debug.WriteLine("Undo");
            listActions.Add(ConstUndoActionE);
            listActions.Add(ConstUndoActionDTer);
            listActions.Add(ConstUndoActionDBis);
            listActions.Add(ConstUndoActionBBis);
            manager.Undo();

            Debug.WriteLine("Undo");
            // nothing
            manager.Undo();

            Debug.WriteLine("Redo");
            listActions.Add(ConstRedoActionBBis);
            listActions.Add(ConstRedoActionDBis);
            listActions.Add(ConstRedoActionDTer);
            listActions.Add(ConstRedoActionE);
            manager.Redo();

            Debug.WriteLine("Redo");
            // nothing
            manager.Redo();

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        private static void Test4()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            listActions.Add(ConstDoActionA);
            manager.Do(TestActionA());
            listActions.Add(ConstDoActionB);
            manager.Do(TestActionB());
            // action cancelled
            try
            {
                manager.Do(TestActionC(null));
            }
            catch (UndoRedoActionCancelledException)
            {
                // nominal case
            }
            listActions.Add(ConstDoActionD);
            manager.Do(TestActionD());

            Debug.WriteLine("===========Undo================");
            manager.UndoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("-------------------------------");
            manager.RedoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("===========Redo================");

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        private static void Test5()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            listActions.Add(ConstDoActionA);
            manager.Do(TestActionA());
            listActions.Add(ConstDoActionB);
            manager.Do(TestActionB());
            using (UndoRedoActionMacroBloc macroBlocC = manager.NewUndoRedoActionMacroBloc(ConstNameC, ConstTitleC))
            {
                // action cancelled
                macroBlocC.Do(TestActionC(null));
            }
            listActions.Add(ConstDoActionD);
            manager.Do(TestActionD());

            Debug.WriteLine("===========Undo================");
            manager.UndoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("-------------------------------");
            manager.RedoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("===========Redo================");

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        private static void Test6()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            listActions.Add(ConstDoActionA);
            manager.Do(TestActionA());
            listActions.Add(ConstDoActionB);
            manager.Do(TestActionB());
            try
            {
                // action failed
                manager.Do(TestActionC(""));
            }
            catch (Exception)
            {
                listActions.Clear();
                _listActions.Clear();
            }
            listActions.Add(ConstDoActionD);
            manager.Do(TestActionD());

            Debug.WriteLine("===========Undo================");
            manager.UndoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("-------------------------------");
            manager.RedoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("===========Redo================");

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        private static void Test7()
        {
            UndoRedoManager manager = new UndoRedoManager();
            manager.Clear();
            _listActions.Clear();
            List<string> listActions = new List<string>();

            listActions.Add(ConstDoActionA);
            manager.Do(TestActionA());
            listActions.Add(ConstDoActionB);
            manager.Do(TestActionB());
            using (UndoRedoActionMacroBloc macroBlocA = manager.NewUndoRedoActionMacroBloc(ConstNameC, ConstTitleC))
            {
                try
                {
                    macroBlocA.Do(TestActionC(""));
                }
                catch (Exception)
                {
                    _listActions.Clear();
                    listActions.Clear();
                }
            }
            listActions.Add(ConstDoActionD);
            manager.Do(TestActionD());

            Debug.WriteLine("===========Undo================");
            manager.UndoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("-------------------------------");
            manager.RedoActions.ForEach(t => Debug.WriteLine(t.Title));
            Debug.WriteLine("===========Redo================");

            Debug.Assert(_listActions.Count == listActions.Count);
            Debug.Assert(_listActions.Zip(listActions, (a1, a2) => a1 == a2).All(b => b));
        }

        private static UndoRedoAction TestAction(string name, string title, string doText, string redoText, string undoText)
        {
            string text = name + " (" + title + ")";
            Action doAction = () => { Debug.WriteLine("doAction " + text); _listActions.Add(doText); };
            Action redoAction = () => { Debug.WriteLine("redoAction " + text); _listActions.Add(redoText); };
            Action undoAction = () => { Debug.WriteLine("undoAction " + text); _listActions.Add(undoText); };

            if (title == null)
                doAction = () => { throw new UndoRedoActionCancelledException(); };
            if (title == "")
                doAction = () => { throw new Exception(); };

            return new UndoRedoAction(name, title, doAction, redoAction, undoAction);
        }

        private static UndoRedoAction TestActionA(string title = ConstTitleA)
        {
            return TestAction(ConstNameA, title, ConstDoActionA, ConstRedoActionA, ConstUndoActionA);
        }

        private static UndoRedoAction TestActionB(string title = ConstTitleB)
        {
            return TestAction(ConstNameB, title, ConstDoActionB, ConstRedoActionB, ConstUndoActionB);
        }

        private static UndoRedoAction TestActionBBis(string title = ConstTitleBBis)
        {
            return TestAction(ConstNameBBis, title, ConstDoActionBBis, ConstRedoActionBBis, ConstUndoActionBBis);
        }

        private static UndoRedoAction TestActionC(string title = ConstTitleC)
        {
            return TestAction(ConstNameC, title, ConstDoActionC, ConstRedoActionC, ConstUndoActionC);
        }

        private static UndoRedoAction TestActionD(string title = ConstTitleD)
        {
            return TestAction(ConstNameD, title, ConstDoActionD, ConstRedoActionD, ConstUndoActionD);
        }

        private static UndoRedoAction TestActionDBis(string title = ConstTitleDBis)
        {
            return TestAction(ConstNameDBis, title, ConstDoActionDBis, ConstRedoActionDBis, ConstUndoActionDBis);
        }

        private static UndoRedoAction TestActionDTer(string title = ConstTitleDTer)
        {
            return TestAction(ConstNameDTer, title, ConstDoActionDTer, ConstRedoActionDTer, ConstUndoActionDTer);
        }

        private static UndoRedoAction TestActionE(string title = ConstTitleE)
        {
            return TestAction(ConstNameE, title, ConstDoActionE, ConstRedoActionE, ConstUndoActionE);
        }

#endif

        #endregion Tests
    }
}