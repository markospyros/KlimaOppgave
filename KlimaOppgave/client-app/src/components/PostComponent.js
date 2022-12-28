import React from "react";

const PostComponent = (props) => {
  return (
    <>
      <div className="card-body">
        <h5 className="card-title">{props.tittel}</h5>
        <p className="card-text text-start">{props.innhold}</p>
        <p className="card-text text-end text-secondary">{props.dato}</p>
      </div>
    </>
  );
};

export default PostComponent;
