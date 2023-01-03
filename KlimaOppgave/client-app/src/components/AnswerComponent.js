import axios from "axios";
import React, { useState } from "react";
import { BiEdit, BiTrash } from "react-icons/bi";

const AnswerComponent = (props) => {
  const [cursor, setCursor] = useState("default");
  const [visibility, setVisibility] = useState("none");

  const onHoverAnswer = () => {
    setVisibility("block");
  };

  const onLeaveAnswer = () => {
    setVisibility("none");
  };

  const onHoverIcon = () => {
    setCursor("pointer");
  };

  const onDelete = (id) => {
    axios.delete(`/slettsvar/${id}`).then(() => props.deleteAnswer(id));
  };

  return (
    <div
      className="card-footer"
      onMouseEnter={onHoverAnswer}
      onMouseLeave={onLeaveAnswer}
    >
      <div className="row">
        <div className="col-9">
          <div>{props.svarInnhold}</div>
        </div>
        <div className={`col-3 text-end d-${visibility}`}>
          <BiTrash
            color="red"
            size={20}
            style={{ cursor: cursor }}
            onMouseEnter={onHoverIcon}
            onClick={() => onDelete(props.svarId)}
          />
        </div>
        <div className="text-end text-secondary">{props.svarDato}</div>
      </div>
    </div>
  );
};

export default AnswerComponent;
