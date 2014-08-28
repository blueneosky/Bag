using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode
{
    [Serializable]
    public class ConditionalCodeNode : BatchCodeNodeBase
    {
        private int? _errorLevelNumber;
        private BatchExpressionBase _fileNameExpression;
        private bool _inverse;
        private BatchExpressionBase _left;
        private BatchExpressionBase _right;

        public ConditionalCodeNode(int errorLevelNumber, bool inverse = false)
        {
            _errorLevelNumber = errorLevelNumber;
            _inverse = inverse;
        }

        public ConditionalCodeNode(BatchExpressionBase left, BatchExpressionBase right, bool inverse = false)
        {
            _left = left;
            _right = right;
            _inverse = inverse;
        }

        public ConditionalCodeNode(LiteralBatch left, BatchExpressionBase right, bool inverse = false)
        {
            _left = new LiteralValueExpression(left);
            _right = right;
            _inverse = inverse;
        }

        public ConditionalCodeNode(LiteralBatch left, LiteralBatch right, bool inverse = false)
        {
            _left = new LiteralValueExpression(left);
            _right = new LiteralValueExpression(right);
            _inverse = inverse;
        }

        public ConditionalCodeNode(string fileName, bool inverse = false)
        {
            _fileNameExpression = new ValueExpression(fileName);
            _inverse = inverse;
        }

        public ConditionalCodeNode(BatchExpressionBase fileNameExpression, bool inverse = false)
        {
            _fileNameExpression = fileNameExpression;
            _inverse = inverse;
        }

        protected ConditionalCodeNode(int? errorLevelNumber, BatchExpressionBase left, BatchExpressionBase right, BatchExpressionBase fileNameExpression, bool inverse)
        {
            _errorLevelNumber = errorLevelNumber;
            _left = left;
            _right = right;
            _fileNameExpression = fileNameExpression;
            _inverse = inverse;
        }

        public int ErrorLevelNumber
        {
            get { return _errorLevelNumber ?? 0; }
        }

        public int? ErrorLevelNumberRaw
        {
            get { return _errorLevelNumber; }
        }

        public BatchExpressionBase FileNameExpression
        {
            get { return _fileNameExpression ?? new ValueExpression(String.Empty); }
        }

        public BatchExpressionBase FileNameExpressionRaw
        {
            get { return _fileNameExpression; }
        }

        public bool Inverse
        {
            get { return _inverse; }
        }

        public bool InverseRaw
        {
            get { return _inverse; }
        }

        public bool IsEqualityTest { get { return !IsErrorLevelTest && !IsFileNameTest; } }

        public bool IsErrorLevelTest { get { return _errorLevelNumber.HasValue; } }

        public bool IsFileNameTest { get { return _fileNameExpression != null; } }

        public BatchExpressionBase Left
        {
            get { return _left ?? new ValueExpression(String.Empty); }
        }

        public BatchExpressionBase LeftRaw
        {
            get { return _left; }
        }

        public BatchExpressionBase Right
        {
            get { return _right ?? new ValueExpression(String.Empty); }
        }

        public BatchExpressionBase RightRaw
        {
            get { return _right; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}