import React from "react";

const AnswerComponent = (props) => {
  return (
    <div className="card-footer">
      <div>{props.svarInnhold}</div>
      <div className="text-end text-secondary">{props.svarDato}</div>
    </div>
  );
};

export default AnswerComponent;
//
