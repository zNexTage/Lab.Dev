import { v4 as uuidv4 } from 'uuid';

class TodoItem {

    constructor(title:string, description:string, id?:string){                
        if(!id){
            id = uuidv4();
        }                   
        
        this._id = id!;
        this._title = title;
        this._description = description;
    }
    
    private _id : string;
    
    public get id() : string {
        return this._id;
    }
    public set id(v : string) {
        this._id = v;
    }

    
    private _title : string;
    public get title() : string {
        return this._title;
    }
    public set title(v : string) {
        this._title = v;
    }
    
    private _description : string;
    public get description() : string {
        return this._description;
    }
    public set description(v : string) {
        this._description = v;
    }
    
}

export default TodoItem;