import axios from "axios";
import React, { useState } from "react";
import { BiEdit, BiTrash } from "react-icons/bi";
import { useNavigate } from "react-router-dom";

const PostComponent = (props) => {
  const [cursor, setCursor] = useState("default");
  const [visibility, setVisibility] = useState("none");

  const navigate = useNavigate();

  const onEdit = (id) => {
    navigate(`/edit/${id}`);
  };

  const onDelete = (id) => {
    axios.delete(`/slettinnlegg/${id}`);
    props.deletePost(id);
  };

  const onHoverPost = () => {
    setCursor("pointer");
    setVisibility("block");
  };

  const onLeavePost = () => {
    setVisibility("none");
  };

  return (
    <>
      <div
        className="card-body"
        style={{ cursor: cursor }}
        onMouseEnter={onHoverPost}
        onMouseLeave={onLeavePost}
      >
        <div className="row">
          <div className="col-9">
            <h5 className="card-title">{props.tittel}</h5>
          </div>
          <div className={`col-3 text-end d-${visibility}`}>
            <BiEdit
              color="blue"
              size={20}
              className="mx-4"
              onClick={() => onEdit(props.id)}
            />
            <BiTrash
              color="red"
              size={20}
              style={{ cursor: cursor }}
              onMouseEnter={() => {
                setCursor("pointer");
              }}
              onClick={() => onDelete(props.id)}
            />
          </div>
        </div>
        <div className="row">
          <div className="col-12">
            <p className="card-text text-start">{props.innhold}</p>
            <p className="card-text text-end text-secondary">{props.dato}</p>
          </div>
        </div>
      </div>
    </>
  );
};

export default PostComponent;
