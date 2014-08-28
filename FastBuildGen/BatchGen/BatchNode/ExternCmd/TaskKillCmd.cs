using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.ExternCmd
{
    [Serializable]
    public class TaskKillCmd : BatchCmdBase
    {
        //[/S système] [/U utilisateur [/P [mot_passe]]]]
        //{ [/FI filtre] [/PID ID_processus | /IM image] } [/T] [/F]
        private bool? _force;

        private BatchExpressionBase _imageName;
        private BatchExpressionBase _passWord;
        private BatchExpressionBase _system;
        private BatchExpressionBase _user;
        private bool? _withChildProcess;

        public TaskKillCmd(
            BatchExpressionBase imageName
            , BatchExpressionBase system = null
            , BatchExpressionBase user = null
            , BatchExpressionBase passWord = null
            , bool? withChildProcess = null
            , bool? force = null
            )
        {
            _imageName = imageName;
            _system = system;
            _user = user;
            _passWord = passWord;
            _withChildProcess = withChildProcess;
            _force = force;
        }

        public bool? Force
        {
            get { return _force; }
        }

        public BatchExpressionBase ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        public BatchExpressionBase PassWord
        {
            get { return _passWord; }
        }

        public BatchExpressionBase System
        {
            get { return _system; }
        }

        public BatchExpressionBase User
        {
            get { return _user; }
        }

        public bool? WithChildProcess
        {
            get { return _withChildProcess; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}