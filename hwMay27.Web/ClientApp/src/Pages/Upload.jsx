import axios from "axios";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";

const Upload = () => {


    const fileRef = useRef();
    const navigate = useNavigate();

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });

    const uploadClick = async () => {
        if (!fileRef.current.files.length) {
            return;
        }
        const file = fileRef.current.files[0];
        const base64 = await toBase64(fileRef.current.files[0]);
        await axios.post('/api/person/addpeople', {base64});
        navigate('/');
    }

    return <div className="row">
    <div className="mt-5 d-flex w-100 justify-content-center align-self-center">
        <div className="row">
            <input ref={fileRef} className="form-control" type="file"/>
        </div>
        <div className="row">
            <div className="col-md-4 offset-md-2">
                <button onClick={uploadClick} className="btn btn-primary">Upload</button>
            </div>
        </div>
    </div>
</div>
}

export default Upload;