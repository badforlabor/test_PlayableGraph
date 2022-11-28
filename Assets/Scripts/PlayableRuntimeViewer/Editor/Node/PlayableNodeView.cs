//=================================================
//
//    Created by jianzhou.yao
//
//=================================================

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

namespace YaoJZ.Playable.Node
{
    public class PlayableNodeView:GraphNodeView<UnityEngine.Playables.Playable>
    {

        private Label _lblInputCount;
        private Label _lblOutputCount;
        private Label _lblPlayState;
        private Label _lblTime;
        private Label _lblValid;
        private Label _lblDone;
        private Label _lblDuration;

        // private Label _lblDepth;

        private Label _lblSpeed;

        private List<Port> _inputs = new List<Port>();
        private List<Port> _outputs = new List<Port>();
        
        public List<Port> Inputs
        {
            get => _inputs;
        }
        
        public List<Port> Outputs
        {
            get => _outputs;
        }

        public override Port GetPort(Direction direction,int index)
        {
            List<Port> ports = direction == Direction.Input ? _inputs : _outputs;
            return ports[index];
        }
        
        public PlayableNodeView(UnityEngine.Playables.Playable data) : base(data)
        {
            titleContainer.style.backgroundColor = GetColor();
            style.color = Color.black;

            this.title = data.GetPlayableType().Name +$"(SiblingIndex:{this.SiblingIndex})";
            var titleLabel = this.mainContainer.Q<Label>("title-label", (string) null);
            titleLabel.style.color = Color.red;

            // _lblDepth = new Label();
            // mainContainer.Add(_lblDepth);
            
            _lblPlayState = new Label();
            _lblPlayState.style.color = Color.white;
            mainContainer.Add(_lblPlayState);
            _lblTime = new Label();
            _lblTime.style.color = Color.white;
            mainContainer.Add(_lblTime);
            _lblInputCount = new Label();
            _lblInputCount.style.color = Color.white;
            mainContainer.Add(_lblInputCount);
            _lblOutputCount = new Label();
            _lblOutputCount.style.color = Color.white;
            mainContainer.Add(_lblOutputCount);
            _lblSpeed = new Label();
            _lblSpeed.style.color = Color.white;
            mainContainer.Add(_lblSpeed);
            
            _lblValid = new Label();
            _lblValid.style.color = Color.white;
            mainContainer.Add(_lblValid);
            _lblDone = new Label();
            _lblDone.style.color = Color.white;
            mainContainer.Add(_lblDone);
            
            _lblDuration = new Label();
            _lblDuration.style.color = Color.white;
            mainContainer.Add(_lblDuration);

            for (int i = 0; i < Data.GetInputCount(); i++)
            {
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
                    typeof(Port));
                port.portName = "input";
                inputContainer.Add(port);
                _inputs.Add(port);
            }
            
            for (int i = 0; i < Data.GetOutputCount(); i++)
            {
                var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,
                    typeof(Port));
                port.portName = "output";
                outputContainer.Add(port);
                _outputs.Add(port);
            }
            CreateChildren();
            
            UpdateView();
        }

        private Color GetColor()
        {
            Type type = Data.GetPlayableType();
            return GetColor(type);
        }

        protected virtual void CreateChildren()
        {
            
        }

        public override void UpdateView()
        {
            _lblPlayState.text = $"PlayState:{Data.GetPlayState()}";
            _lblTime.text = $"Time:{Data.GetTime()}";
            _lblInputCount.text = $"InputCount:{Data.GetInputCount()}";
            _lblOutputCount.text = $"InputCount:{Data.GetOutputCount()}";
            _lblSpeed.text = $"Speed:{Data.GetSpeed()}";
            _lblDone.text = $"IsDone:{Data.IsDone()}";
            _lblValid.text = $"IsValid:{Data.IsValid()}";
            _lblDuration.text = $"Duration:{Data.GetDuration()}";
            //this.title = this.Data.GetPlayableType().Name +$"SiblingIndex:{this.SiblingIndex}";
            // _lblDepth.text = $"Depth:{Depth}";
        }
    }
}