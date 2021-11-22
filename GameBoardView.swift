//
//  GameBoardView.swift
//  Escapani
//
//  Created by Panos Peltekis on 10/6/21.
//

import UIKit

enum ShiftDirection: String {
    case up = "up ↑"
    case right = "right →"
    case down = "down ↓"
    case left = "left ←"
}

protocol GameBoardViewDelegate {
    func gameBoardView(view: GameBoardView, didSwipeInDirection direction: ShiftDirection)
}

final class GameBoardView: UIView {
    
    var delegate: GameBoardViewDelegate?
    private var startLocation: CGPoint = CGPoint.zero
    
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        startLocation = touches.first?.location(in: self) ?? CGPoint.zero
    }
    
    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        let newLocation = touches.first?.location(in: self) ?? CGPoint.zero
        let prevLocation = startLocation
        
        var directionX: ShiftDirection?
        if newLocation.x > prevLocation.x {
            directionX = .right
        } else if newLocation.x == prevLocation.x {
            
        } else {
            directionX = .left
        }
        
        var directionY: ShiftDirection?
        if newLocation.y > prevLocation.y {
            directionY = .down
        } else if newLocation.y == prevLocation.y {
            
        } else {
            directionY = .up
        }
        
        var direction: ShiftDirection?
        if abs(newLocation.x - prevLocation.x) > abs(newLocation.y - prevLocation.y) {
            direction = directionX
        } else {
            direction = directionY
        }
        
        if direction != nil {
            delegate?.gameBoardView(view: self, didSwipeInDirection: direction!)
        }
        
    }
    
}
